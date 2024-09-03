using CaMan.DomainOld.Shared;

namespace CaMan.DomainOld.Users;

public record UserId(Ulid Value) : EntityId(Value);