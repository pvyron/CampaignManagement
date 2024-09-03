using CaMan.DomainOld.Shared;
using CaMan.DomainOld.Users;
using CaMan.PersistenceOld;
using Microsoft.EntityFrameworkCore;

namespace CaMan.ApiOld.Users;

public sealed class RegisterUser(CaManDbContext dbContext)
{
    public sealed record Request(string Email, string ShortName, string Password);
    
    public async Task<IResult> Handle(Request request, CancellationToken cancellationToken)
    {
        if (await dbContext.Users.AnyAsync(u => u.Email.Value.Equals(request.Email, StringComparison.OrdinalIgnoreCase),
                cancellationToken))
        {
            throw new Exception($"Email {request.Email} is already taken");
        }

        if (!Email.Validate(request.Email))
        {
            throw new Exception($"Email {request.Email} is invalid");
        }

        if (!ShortName.Validate(request.ShortName))
        {
            throw new Exception($"ShortName {request.ShortName} is invalid");
        }
        
        var shortName = ShortName.Create(request.ShortName);
        var email = Email.Create(request.Email);
        
        var user = User.Create(shortName, email);

        if (!user.Register(request.Password))
        {
            throw new Exception($"Password {request.Password} is invalid");
        }

        dbContext.Users.Add(user);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            throw new Exception($"Failed to save changes to database: {ex.Message}", ex);
        }
        
        return Results.Ok();
    }
}