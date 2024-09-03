using CaMan.ApiOld.Middlewares;
using CaMan.ApiOld.OpenApi;
using CaMan.ApiOld.Services;
using CaMan.PersistenceOld;
using Carter;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CaManDbContext>((services, optionsBuilder) =>
{
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
    optionsBuilder.UseMySql(builder.Configuration.GetConnectionString("SqlDb"), serverVersion);
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(SecuritySchemas.UserHeader.Name, SecuritySchemas.UserHeader);
    options.AddSecurityDefinition(SecuritySchemas.KeyHeader.Name, SecuritySchemas.KeyHeader);
    options.OperationFilter<AuthHeadersOperationFilter>();
});

builder.Services.AddScoped<IdentityScope>();
builder.Services.AddCarter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<AuthMiddleware>();

app.MapControllers();

app.MapCarter();

app.Run();


// Testing reference point
namespace CaMan.ApiOld
{
    public partial class Program
    {
    };
}