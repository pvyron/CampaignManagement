using System.Net.Http.Json;
using CaMan.Api.Controllers;
using CaMan.IntegrationTests.Users.Models;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace CaMan.IntegrationTests.Users;

public class UpdateIntegrationTests : BaseIntegrationTest
{
    public UpdateIntegrationTests(IntegrationTestApiFactory apiFactory, ITestOutputHelper output) : base(apiFactory, output)
    {
    }
    
    [Fact]
    public async Task Update_ShouldUpdate_EmailOfExistingUser_ToDatabase()
    {
        // Arrange
        var existingUser = await UserHelperMethods.CreateRandomUserInDb(_apiDbContext);
        
        var shortName = existingUser.ShortName.Value;
        var email = "test@test.com";

        // Act
        var httpResponse =
            await _apiClient.PutAsJsonAsync($"/api/Users/{existingUser.Id.Value}", 
                new UpdateUser(null, email, null));

        Assert.True(httpResponse.IsSuccessStatusCode);
        
        var updatedUser = await httpResponse.Content.ReadFromJsonAsync<CreatedTestUser>();

        //Assert
        Assert.NotNull(updatedUser);
        Assert.Equal(existingUser.Id, updatedUser.Id);
        Assert.Equal(shortName, updatedUser.ShortName.Value);
        Assert.Equal(email, updatedUser.Email.Value);
        
        var fetchedUser = await (await _apiClient.GetAsync($"api/Users/{existingUser.Id.Value}")).Content.ReadFromJsonAsync<CreatedTestUser>();
        
        Assert.NotNull(fetchedUser);
        Assert.Equal(updatedUser.Id, fetchedUser.Id);
        Assert.Equal(updatedUser.ShortName.Value, fetchedUser.ShortName.Value);
        Assert.Equal(updatedUser.Email.Value, fetchedUser.Email.Value);

        var dbUser = await _apiDbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == updatedUser.Id);
        
        Assert.NotNull(dbUser);
        Assert.Equal(updatedUser.Id, dbUser.Id);
        Assert.Equal(updatedUser.ShortName.Value, dbUser.ShortName.Value);
        Assert.Equal(updatedUser.Email.Value, dbUser.Email.Value);
    }

    [Theory]
    [InlineData(["testtest.com"])]
    [InlineData(["test.com@test"])]
    [InlineData(["noTest-yahoo.ln"])]
    public async Task Create_ShouldFailToUpdate_ExistingUser_InvalidEmail(string invalidEmail)
    {
        // Arrange
        var existingUser = await UserHelperMethods.CreateRandomUserInDb(_apiDbContext);

        // Act
        var httpResponse =
            await _apiClient.PutAsJsonAsync($"/api/Users/{existingUser.Id.Value}", new { Email = invalidEmail });
        
        var savedUser = await _apiDbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == existingUser.Id);

        //Assert
        Assert.False(httpResponse.IsSuccessStatusCode);

        Assert.NotNull(savedUser);
        Assert.Equal(existingUser.ShortName, savedUser.ShortName);
        Assert.Equal(existingUser.Email, savedUser.Email);
    }
}