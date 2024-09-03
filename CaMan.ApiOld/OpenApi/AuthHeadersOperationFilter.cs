using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CaMan.ApiOld.OpenApi;

public class AuthHeadersOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Security ??= new List<OpenApiSecurityRequirement>();

        var cMUser = new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "cMUser" } };
        var cMKey = new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "cMKey" } };
        operation.Security.Add(new OpenApiSecurityRequirement
        {
            [cMUser] = new List<string>(),
            [cMKey] = new List<string>()
        });
    }
}