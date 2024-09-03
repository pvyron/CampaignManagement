using Bogus;
using CaMan.DomainOld.Shared;
using CaMan.DomainOld.Users;
using CaMan.PersistenceOld;

namespace CaMan.IntegrationTestsOld.Users;

public static class UserHelperMethods
{
    private static User AddUserInDbCollectionInternal(CaManDbContext dbContext, string shortName, string email)
    {
        var createdUser = User.Create(ShortName.Create(shortName), Email.Create(email));
        
        dbContext.Users.Add(createdUser);

        return createdUser;
    }
    
    public static async Task<User> CreateUserInDb(CaManDbContext dbContext, string shortName, string email)
    {
        var createdUser = AddUserInDbCollectionInternal(dbContext, shortName, email);

        await dbContext.SaveChangesAsync();

        return createdUser;
    }
    
    public static async Task<User> CreateRandomUserInDb(CaManDbContext dbContext)
    {
        var createdUser = AddUserInDbCollectionInternal(dbContext, $"test_{Ulid.NewUlid()}", new Faker().Internet.Email());

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            var a = ex.Message;
            throw;
        }

        return createdUser;
    }
    
    public static async Task<User[]> CreateRandomUsersInDb(CaManDbContext dbContext, int count = 10)
    {
        var createdUsers = new User[count];
        for (int i = 0; i < count; i++)
        {
            createdUsers[i] = AddUserInDbCollectionInternal(dbContext, $"test_{i}", new Faker().Internet.Email());
        }
        
        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            var a = ex.Message;
            throw;
        }

        return createdUsers;
    }
}