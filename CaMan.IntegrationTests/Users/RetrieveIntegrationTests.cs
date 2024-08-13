﻿using System.Net;
using System.Net.Http.Json;
using CaMan.IntegrationTests.Users.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CaMan.IntegrationTests.Users;

public class RetrieveIntegrationTests : BaseIntegrationTest
{
    private readonly ILogger<RetrieveIntegrationTests> _logger;
    
    public RetrieveIntegrationTests(IntegrationTestApiFactory apiFactory) : base(apiFactory)
    {
        _logger = _apiScope.ServiceProvider.GetRequiredService<ILogger<RetrieveIntegrationTests>>();
    }
    
    [Theory]
    [InlineData([5, 10])]
    [InlineData([10, 5])]
    [InlineData([7, 7])]
    [InlineData([1, 0])]
    [InlineData([0, 1])]
    public async Task Retrieve_ShouldReturn_SpecificCount_CreatedUsers_FromDatabase(int usersToCreate, int usersToFetch)
    {
        // Arrange
        await _apiDbContext.Users.AsQueryable().ExecuteDeleteAsync();
        
        var createdUsers = await UserHelperMethods.CreateRandomUsersInDb(_apiDbContext, usersToCreate);

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
    public async Task Retrieve_ShouldReturn_ExistingUser_FromDatabase()
    {
        // Arrange
        var existingUsers = await UserHelperMethods.CreateRandomUsersInDb(_apiDbContext, Random.Shared.Next(5, 15));
        
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
    
    [Fact]
    public async Task Retrieve_ShouldFailToReturn_NonExistingUser()
    {
        // Arrange
        var existingUsers = await UserHelperMethods.CreateRandomUsersInDb(_apiDbContext, Random.Shared.Next(5, 15));

        for (int e = 0; e < existingUsers.Length; e++)
        {
            // Act
            var httpResponse = await _apiClient.GetAsync($"/api/Users/{Ulid.NewUlid()}");
        
            // Assert
            Assert.False(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
        }
    }
    
    [Fact]
    public async Task Retrieve_ShouldFail_InvalidIdFormat()
    {
        // Arrange
        var existingUsers = await UserHelperMethods.CreateRandomUsersInDb(_apiDbContext, Random.Shared.Next(4, 8));

        for (var i = 0; i < existingUsers.Length; i++)
        {
            // Act
            var httpResponse = await _apiClient.GetAsync($"/api/Users/{Ulid.NewUlid().ToString()[1..]}");
        
            // Assert
            Assert.False(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        }
    }
}