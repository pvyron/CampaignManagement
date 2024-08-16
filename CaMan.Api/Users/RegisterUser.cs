namespace CaMan.Api.Users;

public sealed class RegisterUser
{
    public async Task<IResult> Handle()
    {
        await Task.Delay(150);
        return Results.Ok();
    }
}