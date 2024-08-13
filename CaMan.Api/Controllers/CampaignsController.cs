using CaMan.Domain.Campaigns;
using CaMan.Domain.Shared;
using CaMan.Domain.Users;
using CaMan.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaMan.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CampaignsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromServices] CaManDbContext dbContext, CancellationToken cancellationToken, [FromQuery(Name = "size")] int size = 10)
    {
        var campaigns = await dbContext.Campaigns
            .Include(c => c.CampaignContacts)
            .AsNoTracking()
            .Take(size)
            .ToListAsync(cancellationToken);

        return Ok(campaigns);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] string id, [FromServices] CaManDbContext dbContext, CancellationToken cancellationToken)
    {
        if (!Ulid.TryParse(id, out var ulId))
        {
            return BadRequest();
        }
        
        var campaign = await dbContext.Campaigns
            .Include(c => c.CampaignContacts)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == new CampaignId(ulId), cancellationToken);

        if (campaign is null)
        {
            return NotFound();
        }

        return Ok(campaign);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUser createUser, [FromServices] CaManDbContext dbContext, CancellationToken cancellationToken)
    {
        return await Task.FromResult(Problem("Not yet implemented"));

        // var shortName = ShortName.Create(createUser.shortName);
        // var email = Email.Create(createUser.email);
        //
        // var newUser = CaMan.Domain.Users.User.Create(shortName, email);
        // dbContext.Users.Add(newUser);
        // await dbContext.SaveChangesAsync(cancellationToken);
        //
        // return Ok(newUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateUser updateUser, [FromServices] CaManDbContext dbContext, CancellationToken cancellationToken)
    {
        if (!Ulid.TryParse(id, out var ulId))
        {
            return BadRequest();
        }
        
        var existingCampaign = await dbContext.Campaigns
            .Include(c => c.CampaignContacts)
            .FirstOrDefaultAsync(c => c.Id == new CampaignId(ulId), cancellationToken);

        if (existingCampaign is null)
        {
            return NotFound();
        }

        return Problem("Not implemented fully yet");

        // if (!string.IsNullOrWhiteSpace(updateUser.shortName))
        // {
        //     var newShortName = ShortName.Create(updateUser.shortName);
        //
        //     existingUser.UpdateShortName(newShortName);
        // }
        //
        // if (!string.IsNullOrWhiteSpace(updateUser.email))
        // {
        //     var newEmail = Email.Create(updateUser.email);
        //
        //     existingUser.UpdateEmail(newEmail);
        // }
        //
        // await dbContext.SaveChangesAsync(cancellationToken);
        //
        // return Ok(existingUser);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id, [FromServices] CaManDbContext dbContext, CancellationToken cancellationToken)
    {
        if (!Ulid.TryParse(id, out var ulId))
        {
            return BadRequest();
        }
        
        var existingCampaign = await dbContext.Campaigns
            .FirstOrDefaultAsync(c => c.Id == new CampaignId(ulId), cancellationToken);

        if (existingCampaign is null)
        {
            return NotFound();
        }

        dbContext.Campaigns.Remove(existingCampaign);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Ok();
    }
}