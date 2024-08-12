using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CaMan.IntegrationTests.Users;

public class DeleteIntegrationTests : BaseIntegrationTest
{
    private readonly ILogger<DeleteIntegrationTests> _logger;
    
    public DeleteIntegrationTests(IntegrationTestApiFactory apiFactory) : base(apiFactory)
    {
        _logger = _apiScope.ServiceProvider.GetRequiredService<ILogger<DeleteIntegrationTests>>();
    }
}