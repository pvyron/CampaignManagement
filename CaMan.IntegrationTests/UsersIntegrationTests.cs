using System.Net;
using System.Net.Http.Json;
using CaMan.Api.Controllers;
using CaMan.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CaMan.IntegrationTests;

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
        var createUser = new CreateUser("test", "test@test.com");

        // Act
        var httpResult = await _apiClient.PostAsJsonAsync("/api/Users", createUser);

        var resultAsString = await httpResult.Content.ReadAsStringAsync();
        var createdUser = await httpResult.Content.ReadFromJsonAsync<CreatedTestUser>();

        //Assert
        var createdId = Assert.NotNull(createdUser?.Id?.Value);

        Assert.Equal(createUser.shortName, createdUser.ShortName.Value);
        Assert.Equal(createUser.email, createdUser.Email.Value);

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
        var createUser = new CreateUser("test", invalidEmail);

        // Act
        var existingUsers = await _apiDbContext.Users.CountAsync();
        
        var httpResult = await _apiClient.PostAsJsonAsync("/api/Users", createUser);

        var newUsers = await _apiDbContext.Users.CountAsync();
        
        //Assert
        Assert.False(httpResult.IsSuccessStatusCode);
        
        Assert.Equal(existingUsers, newUsers);
    }

    private record CreatedTestUser(UserId Id, GenericStringValueType ShortName, GenericStringValueType Email);
    private record GenericStringValueType(string Value);

}