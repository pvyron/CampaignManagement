using CaMan.Domain.Shared;
using CaMan.Domain.Users;
using CaMan.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaMan.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromServices] CaManDbContext dbContext, CancellationToken cancellationToken, [FromQuery(Name = "size")] int size = 10)
    {
        var users = await dbContext.Users
            .Include(u => u.ContactInfo)
            .AsNoTracking()
            .Take(size)
            .ToListAsync(cancellationToken);

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] string id, [FromServices] CaManDbContext dbContext, CancellationToken cancellationToken)
    {
        if (!Ulid.TryParse(id, out var ulId))
        {
            return BadRequest();
        }
        
        var user = await dbContext.Users
            .Include(u => u.ContactInfo)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == new UserId(ulId), cancellationToken);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUser createUser, [FromServices] CaManDbContext dbContext, CancellationToken cancellationToken)
    {
        var shortName = ShortName.Create(createUser.shortName);
        var email = Email.Create(createUser.email);

        var newUser = CaMan.Domain.Users.User.Create(shortName, email);
        dbContext.Users.Add(newUser);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Ok(newUser);
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}