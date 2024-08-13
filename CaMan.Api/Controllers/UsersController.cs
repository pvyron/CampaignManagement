using CaMan.Domain.Shared;
using CaMan.Domain.Users;
using CaMan.Persistence;
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
    public async Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateUser updateUser, [FromServices] CaManDbContext dbContext, CancellationToken cancellationToken)
    {
        if (!Ulid.TryParse(id, out var ulId))
        {
            return BadRequest();
        }
        
        var existingUser = await dbContext.Users
            .Include(u => u.ContactInfo)
            .FirstOrDefaultAsync(u => u.Id == new UserId(ulId), cancellationToken);

        if (existingUser is null)
        {
            return NotFound();
        }

        if (!string.IsNullOrWhiteSpace(updateUser.shortName))
        {
            var newShortName = ShortName.Create(updateUser.shortName);

            existingUser.UpdateShortName(newShortName);
        }

        if (!string.IsNullOrWhiteSpace(updateUser.email))
        {
            var newEmail = Email.Create(updateUser.email);

            existingUser.UpdateEmail(newEmail);
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return Ok(existingUser);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id, [FromServices] CaManDbContext dbContext, CancellationToken cancellationToken)
    {
        if (!Ulid.TryParse(id, out var ulId))
        {
            return BadRequest();
        }
        
        var existingUser = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == new UserId(ulId), cancellationToken);

        if (existingUser is null)
        {
            return NotFound();
        }

        dbContext.Users.Remove(existingUser);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Ok();
    }
}