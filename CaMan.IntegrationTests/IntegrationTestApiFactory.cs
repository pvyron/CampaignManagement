using CaMan.Api;
using CaMan.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MySql;

namespace CaMan.IntegrationTests;

public class IntegrationTestApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MySqlContainer 
        _dbContainer = new MySqlBuilder()
            .WithImage("mysql:8.0")
            .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var dbDescriptor = services
                .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<CaManDbContext>));

            if (dbDescriptor is not null)
            {
                services.Remove(dbDescriptor);
            }

            services.AddDbContext<CaManDbContext>(optionsBuilder =>
            {
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
                optionsBuilder.UseMySql(_dbContainer.GetConnectionString(), serverVersion);
            });
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CaManDbContext>();
        await dbContext.Database.MigrateAsync();
    }

    public new Task DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }
}