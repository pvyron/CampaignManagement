using System.Net.Http.Json;
using Bogus;
using CaMan.Api.Controllers;
using CaMan.Domain.Users;

namespace CaMan.IntegrationTests.Users;

public static class UserHelperMethods
{
    public static Task<HttpResponseMessage> TryCreateUser(HttpClient apiClient, string shortName, string email)
    {
        var createUser = new CreateUser(shortName, email);
        
        return apiClient.PostAsJsonAsync("/api/Users", createUser);
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
}