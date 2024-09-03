using CaMan.DomainOld.Users;

namespace CaMan.IntegrationTestsOld.Users.Models;

public record CreatedTestUser(UserId Id, GenericStringValueType ShortName, GenericStringValueType Email);