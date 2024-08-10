using System.Net.Http.Json;
using CaMan.Api.Controllers;
using CaMan.Domain.Shared;
using CaMan.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CaMan.IntegrationTests;

public class UsersIntegrationTests : BaseIntegrationTest
{
    public UsersIntegrationTests(IntegrationTestApiFactory apiFactory) : base(apiFactory)
    {
    }

    [Fact]
    public async Task Create_ShouldAdd_NewUser_ToDatabase()
    {
        // Arrange
        var createUser = new CreateUser("test", "test@test.com");

        // Act
        var httpResult = await _apiClient.PostAsJsonAsync("/api/Users", createUser);
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

    private record CreatedTestUser(UserId Id, GenericStringValueType ShortName, GenericStringValueType Email);
    private record GenericStringValueType(string Value);

}