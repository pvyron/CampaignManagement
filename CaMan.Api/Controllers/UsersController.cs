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
    // GET: api/<UsersController>
    [HttpGet]
    public async Task<IActionResult> Get([FromServices] CaManDbContext dbContext,
        [FromQuery(Name = "size")] int size = 10)
    {
        var users = await dbContext.Users
            .Include(u => u.ContactInfo)
            .AsNoTracking()
            .Take(size)
            .ToListAsync();

        return users.Count switch
        {
            0 => NotFound(),
            1 => Ok(users[0]),
            _ => Ok(users)
        };
    }

    // GET api/<UsersController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] string id, [FromServices] CaManDbContext dbContext)
    {
        if (!Ulid.TryParse(id, out var ulId))
        {
            return BadRequest();
        }
            
        var user = await dbContext.Users
            .Include(u => u.ContactInfo)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == new UserId(ulId));

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    // POST api/<UsersController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUser createUser, [FromServices] CaManDbContext dbContext)
    {
        var shortName = ShortName.Create(createUser.shortName);
        var email = Email.Create(createUser.email);

        var newUser = CaMan.Domain.Users.User.Create(shortName, email);
        dbContext.Users.Add(newUser);
        await dbContext.SaveChangesAsync();

        return Ok(newUser);
    }

    // PUT api/<UsersController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<UsersController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}