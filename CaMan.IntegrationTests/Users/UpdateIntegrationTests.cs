using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CaMan.IntegrationTests.Users;

public class UpdateIntegrationTests : BaseIntegrationTest
{
    private readonly ILogger<UpdateIntegrationTests> _logger;
    
    public UpdateIntegrationTests(IntegrationTestApiFactory apiFactory) : base(apiFactory)
    {
        _logger = _apiScope.ServiceProvider.GetRequiredService<ILogger<UpdateIntegrationTests>>();
    }
}