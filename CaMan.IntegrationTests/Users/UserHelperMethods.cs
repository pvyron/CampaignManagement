using System.Net.Http.Json;
using Bogus;
using CaMan.Api.Controllers;
using CaMan.Domain.Shared;
using CaMan.Domain.Users;
using CaMan.IntegrationTests.Users.Models;
using CaMan.Persistance;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CaMan.IntegrationTests.Users;

public static class UserHelperMethods
{
    public static Task<HttpResponseMessage> TryCreateUser(HttpClient apiClient, string shortName, string email)
    {
        var createUser = new CreateUser(shortName, email);
        
        return apiClient.PostAsJsonAsync("/api/Users", createUser);
    }
    public static EntityEntry<User> TryCreateUserInDb(CaManDbContext dbContext, string shortName, string email)
    {
        var createUser = new CreateUser(shortName, email);
        
        return dbContext.Users.Add(User.Create(ShortName.Create(shortName), Email.Create(email)));
    }
    
    public static Task<HttpResponseMessage> TryCreateRandomUser(HttpClient apiClient)
    {
        return TryCreateUser(apiClient, "test", new Faker().Internet.Email());
    }

    public static async Task<CreatedTestUser[]> CreateRandomUsers(HttpClient apiClient, int count = 10)
    {
        var createdUsers = new CreatedTestUser[count];
        
        for (int i = 0; i < count; i++)
        {
            var httpResponse = await TryCreateUser(apiClient, $"test_{i}", new Faker().Internet.Email());

            Assert.True(httpResponse.IsSuccessStatusCode);
            
            createdUsers[i] = (await httpResponse.Content.ReadFromJsonAsync<CreatedTestUser>())!;
        }

        return createdUsers;
    }

    public static async Task<User[]> CreateRandomUsersInDb(CaManDbContext dbContext, int count = 10)
    {
        var createdUsers = new User[count];
        
        for (int i = 0; i < count; i++)
        {
            var createdUser = TryCreateUserInDb(dbContext, $"test_{i}", new Faker().Internet.Email());

            createdUsers[i] = createdUser.Entity;
        }

        var savedEntities = await dbContext.SaveChangesAsync();
        
        return createdUsers;
    }
}