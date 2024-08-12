using System.Net.Http.Json;
using CaMan.Api.Controllers;
using CaMan.Domain.Shared;
using CaMan.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CaMan.IntegrationTests.Users;

public class UsersIntegrationTests : BaseIntegrationTest
{
    private readonly ILogger<UsersIntegrationTests> _logger;
    public UsersIntegrationTests(IntegrationTestApiFactory apiFactory) : base(apiFactory)
    {
        _logger = _apiScope.ServiceProvider.GetRequiredService<ILogger<UsersIntegrationTests>>();
    }

    [Fact]
    public async Task Create_ShouldAdd_NewUser_ToDatabase()
    {
        // Arrange
        var shortName = "test";
        var email = "test@test.com";

        // Act
        var httpResponse = await UserHelperMethods.TryCreateUser(_apiClient, shortName, email);

        Assert.True(httpResponse.IsSuccessStatusCode);
        
        var createdUser = await httpResponse.Content.ReadFromJsonAsync<CreatedTestUser>();

        //Assert
        var createdId = Assert.NotNull(createdUser?.Id?.Value);

        Assert.Equal(shortName, createdUser.ShortName.Value);
        Assert.Equal(email, createdUser.Email.Value);

        var dbUser = await _apiDbContext.Users.FirstOrDefaultAsync(u => u.Id == createdUser.Id);

        Assert.NotNull(dbUser);
        Assert.Equal(createdUser.ShortName.Value, dbUser.ShortName.Value);
        Assert.Equal(createdUser.Email.Value, dbUser.Email.Value);
    }

    [Theory]
    [InlineData(["testtest.com"])]
    [InlineData(["test.com@test"])]
    [InlineData(["noTest-yahoo.ln"])]
    public async Task Create_ShouldFailToAdd_NewUser_InvalidEmail(string invalidEmail)
    {
        // Arrange
        var shortName = "test";

        // Act
        var existingUsers = await _apiDbContext.Users.CountAsync();

        var httpResponse = await UserHelperMethods.TryCreateUser(_apiClient, shortName, invalidEmail);

        var newUsers = await _apiDbContext.Users.CountAsync();
        
        //Assert
        Assert.False(httpResponse.IsSuccessStatusCode);
        
        Assert.Equal(existingUsers, newUsers);
    }

    [Theory]
    [InlineData([5, 10])]
    [InlineData([10, 5])]
    [InlineData([7, 7])]
    [InlineData([1, 0])]
    [InlineData([0, 1])]
    public async Task Fetch_ShouldReturn_SpecificCount_CreatedUsers_FromDatabase(int usersToCreate, int usersToFetch)
    {
        // Arrange
        await _apiDbContext.Users.AsQueryable().ExecuteDeleteAsync();
        
        var createdUsers = await UserHelperMethods.CreateRandomUsers(_apiClient, usersToCreate);

        // Act
        var httpResponse = await _apiClient.GetAsync($"api/Users?size={usersToFetch}");

        Assert.True(httpResponse.IsSuccessStatusCode);

        var fetchedUsers = await httpResponse.Content.ReadFromJsonAsync<CreatedTestUser[]>();
        
        // Assert
        Assert.Equal(usersToCreate, createdUsers.Length);
        Assert.NotNull(fetchedUsers);
        
        Assert.Equal(Math.Min(usersToCreate, usersToFetch), fetchedUsers.Length);
    }

    [Fact]
    public async Task Fetch_ShouldReturn_ExistingUser_FromDatabase()
    {
        // Arrange
        var existingUsers = await UserHelperMethods.CreateRandomUsers(_apiClient, Random.Shared.Next(4, 8));
        
        foreach (var existingUser in existingUsers)
        {
            // Act
            var httpResponse = await _apiClient.GetAsync($"/api/Users/{existingUser.Id.Value}");
        
            // Assert
            Assert.True(httpResponse.IsSuccessStatusCode);

            var fetchedUser = await httpResponse.Content.ReadFromJsonAsync<CreatedTestUser>();

            Assert.NotNull(fetchedUser);
            Assert.Equal(existingUser.Id, fetchedUser.Id);
            Assert.Equal(existingUser.ShortName.Value, fetchedUser.ShortName.Value);
            Assert.Equal(existingUser.Email.Value, fetchedUser.Email.Value);
        }
    }
}

public record CreatedTestUser(UserId Id, GenericStringValueType ShortName, GenericStringValueType Email);
public record GenericStringValueType(string Value);