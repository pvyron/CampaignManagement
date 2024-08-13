using System.Net;
using Microsoft.EntityFrameworkCore;
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

    [Fact]
    public async Task Delete_ShouldDelete_ExistingUser_FromDatabase()
    {
        // Arrange
        await _apiDbContext.Users.ExecuteDeleteAsync();
        var createdUsers = await UserHelperMethods.CreateRandomUsersInDb(_apiDbContext, Random.Shared.Next(5, 15));

        // Act
        foreach (var createdUser in createdUsers)
        {
            var existingUsersCount = await _apiDbContext.Users.CountAsync();

            var shouldDelete = Random.Shared.Next(1, 11) >= 5;
            
            if (shouldDelete)
            {
                var httpResponse = await _apiClient.DeleteAsync($"api/Users/{createdUser.Id.Value}");
                
                Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            }
          
            var currentUserCount = await _apiDbContext.Users.CountAsync();

            // Assert
            if (shouldDelete)
            {
                Assert.Null(await _apiDbContext.Users.FirstOrDefaultAsync(u => u.Id == createdUser.Id));
                Assert.Equal(existingUsersCount, currentUserCount + 1);
            }
            else
            {
                Assert.NotNull(await _apiDbContext.Users.FirstOrDefaultAsync(u => u.Id == createdUser.Id));
                Assert.Equal(existingUsersCount, currentUserCount);
            }
        }
    }
    
    [Fact]
    public async Task Delete_ShouldFail_InvalidIdFormat()
    {
        // Arrange
        var existingUsers = await UserHelperMethods.CreateRandomUsersInDb(_apiDbContext, Random.Shared.Next(5, 15));

        for (var i = 0; i < existingUsers.Length; i++)
        {
            // Act
            var httpResponse = await _apiClient.DeleteAsync($"/api/Users/{Ulid.NewUlid().ToString()[1..]}");
        
            // Assert
            Assert.False(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        }
    }
    
    [Fact]
    public async Task Delete_ShouldFail_NonExistingUser()
    {
        // Arrange
        var existingUsers = await UserHelperMethods.CreateRandomUsersInDb(_apiDbContext, Random.Shared.Next(5, 15));

        for (int e = 0; e < existingUsers.Length; e++)
        {
            // Act
            var httpResponse = await _apiClient.DeleteAsync($"/api/Users/{Ulid.NewUlid()}");
        
            // Assert
            Assert.False(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
        }
    }
}