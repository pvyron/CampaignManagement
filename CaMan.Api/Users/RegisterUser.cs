using CaMan.Api.Abstractions;
using CaMan.Domain.Shared;
using CaMan.Domain.Users;
using CaMan.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CaMan.Api.Users;

public static class RegisterUser
{
    public sealed record Request(string Email, string? ShortName, string Password, string InvitationToken);

    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(r => r.Email).EmailAddress();
            RuleFor(r => r.Password).NotEmpty();
            RuleFor(r => r.InvitationToken).NotEmpty();
        }
    }
    
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("api/Users/Register", Handler);
        }
    }
    
    public static async Task<IResult> Handler(Request request, CaManDbContext dbContext)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email.Value == request.Email);

        if (user is null)
        {
            if (string.IsNullOrWhiteSpace(request.ShortName))
            {
                return Results.BadRequest("missing shortname");
            }
            
            var email = Email.Create(request.Email);
            var shortName = ShortName.Create(request.ShortName);

            user = User.Create(shortName, email);
            dbContext.Users.Add(user);
        }

        if (!user.Register(request.Password))
        {
            return Results.BadRequest("missing shortname");
        }

        await dbContext.SaveChangesAsync();
        
        return Results.Ok();
    }
}