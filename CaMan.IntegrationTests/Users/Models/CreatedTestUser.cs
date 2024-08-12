using CaMan.Domain.Users;

namespace CaMan.IntegrationTests.Users.Models;

public record CreatedTestUser(UserId Id, GenericStringValueType ShortName, GenericStringValueType Email);