using CaMan.Persistance;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MySql;

namespace CaMan.IntegrationTests;

public class IntegrationTestApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    //private readonly MySqlContainer _dbContainer = new MySqlContainer(new MySqlConfiguration("test", "test", "test"));
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            // var dbDescriptor = services
            //     .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<CaManDbContext>));
            //
            // if (dbDescriptor is not null)
            // {
            //     services.Remove(dbDescriptor);
            // }
            //
            // services.AddDbContext<CaManDbContext>(optionsBuilder =>
            // {
            //     optionsBuilder.UseSqlite(_dbContainer.GetConnectionString());
            // });
        });
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;// _dbContainer.StartAsync();
    }

    public new Task DisposeAsync()
    {
        return Task.CompletedTask;// _dbContainer.StopAsync();
    }
}