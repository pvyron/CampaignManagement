using CaMan.Domain.Shared;

namespace CaMan.Domain.ElectoralBodies;

public sealed record ElectoralBodyId(Ulid Value) : EntityId(Value);