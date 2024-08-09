using CaMan.Api.Middlewares;
using CaMan.Api.OpenApi;
using CaMan.Api.Services;
using CaMan.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CaManDbContext>((services, optionsBuilder) =>
{
    optionsBuilder.UseSqlite(builder.Configuration.GetConnectionString("SqlDb"));
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