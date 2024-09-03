using CaMan.DomainOld.Shared;

namespace CaMan.DomainOld.ElectoralBodies;

public sealed record ElectoralBodyId(Ulid Value) : EntityId(Value);