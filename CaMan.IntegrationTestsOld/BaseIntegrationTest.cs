using CaMan.PersistenceOld;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace CaMan.IntegrationTestsOld;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestApiFactory>
{
    protected readonly HttpClient _apiClient;
    protected readonly CaManDbContext _apiDbContext;
    protected readonly IServiceScope _apiScope;
    
    protected BaseIntegrationTest(IntegrationTestApiFactory apiFactory, ITestOutputHelper output)
    {
        _apiClient = apiFactory.Server.CreateClient();
        _apiScope = apiFactory.Services.CreateScope();
        _apiDbContext = _apiScope.ServiceProvider.GetRequiredService<CaManDbContext>();
    }
}