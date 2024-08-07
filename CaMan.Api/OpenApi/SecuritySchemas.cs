using Microsoft.OpenApi.Models;

namespace CaMan.Api.OpenApi;

public static class SecuritySchemas
{
    public static readonly OpenApiSecurityScheme UserHeader = new()
    {
        Name = "cMUser",
        In = ParameterLocation.Header,
        Description = "Username in the form of email",
        Type = SecuritySchemeType.ApiKey
    };

    public static readonly OpenApiSecurityScheme KeyHeader = new()
    {
        Name = "cMKey",
        In = ParameterLocation.Header,
        Description = "Access key as BASE64 string",
        Type = SecuritySchemeType.ApiKey
    };
}