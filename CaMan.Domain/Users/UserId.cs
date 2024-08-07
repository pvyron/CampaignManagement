using CaMan.Domain.Shared;

namespace CaMan.Domain.Users;

public record UserId(Ulid Value) : EntityId(Value);