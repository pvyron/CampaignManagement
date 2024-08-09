using CaMan.Api.Services;
using CaMan.Domain.Shared;
using CaMan.Persistance;
using Microsoft.AspNetCore.Mvc;

namespace CaMan.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpPost]
    public async Task<IResult> TestMe([FromBody] CreateUser userInfo, [FromServices] CaManDbContext dbContext)
    {
        var shortName = ShortName.Create(userInfo.shortName);
        var email = Email.Create(userInfo.email);

        var newUser = Domain.Users.User.Create(shortName, email);
        dbContext.Users.Add(newUser);
        await dbContext.SaveChangesAsync();
        
        return Results.Ok();
    }

}
public record CreateUser(string shortName, string email);