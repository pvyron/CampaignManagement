using CaMan.Api.Middlewares;
using CaMan.Api.OpenApi;
using CaMan.Api.Services;
using CaMan.Persistence;
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<AuthMiddleware>();

app.MapControllers();

app.Run();


// Testing reference point
namespace CaMan.Api
{
    public partial class Program
    {
    };
}