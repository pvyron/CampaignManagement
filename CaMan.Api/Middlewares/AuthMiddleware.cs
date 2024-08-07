using CaMan.Api.Services;

namespace CaMan.Api.Middlewares;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AuthMiddleware> _logger;

    public AuthMiddleware(RequestDelegate next, ILogger<AuthMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, IdentityScope identityScope)
    {
        var emailHeader = context.Request.Headers["cMUser"].FirstOrDefault();
        var keyHeader = context.Request.Headers["cMKey"].FirstOrDefault();

        if (string.IsNullOrEmpty(emailHeader) || string.IsNullOrEmpty(keyHeader))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }
        
        identityScope.Setup(emailHeader, keyHeader);

        await _next(context);
    }
}