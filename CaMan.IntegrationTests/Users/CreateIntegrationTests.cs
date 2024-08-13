using System.Net.Http.Json;
using CaMan.Api.Controllers;
using CaMan.IntegrationTests.Users.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CaMan.IntegrationTests.Users;

public class CreateIntegrationTests : BaseIntegrationTest
{
    private readonly ILogger<CreateIntegrationTests> _logger;
    
    public CreateIntegrationTests(IntegrationTestApiFactory apiFactory) : base(apiFactory)
    {
        _logger = _apiScope.ServiceProvider.GetRequiredService<ILogger<CreateIntegrationTests>>();
    }
    
    [Fact]
    public async Task Create_ShouldAdd_NewUser_ToDatabase()
    {
        // Arrange
        var shortName = "test";
        var email = "test@test.com";

        var createUser = new CreateUser(shortName, email);

        // Act
        var httpResponse = await _apiClient.PostAsJsonAsync("/api/Users", createUser);

        Assert.True(httpResponse.IsSuccessStatusCode);
        
        var createdUser = await httpResponse.Content.ReadFromJsonAsync<CreatedTestUser>();

        //Assert
        Assert.NotNull(createdUser);
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

        var createUser = new CreateUser(shortName, invalidEmail);
        
        // Act
        var existingUsers = await _apiDbContext.Users.CountAsync();

        var httpResponse = await _apiClient.PostAsJsonAsync("/api/Users", createUser);

        var newUsers = await _apiDbContext.Users.CountAsync();
        
        //Assert
        Assert.False(httpResponse.IsSuccessStatusCode);
        
        Assert.Equal(existingUsers, newUsers);
    }
}