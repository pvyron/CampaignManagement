using CaMan.DomainOld.Shared;

namespace CaMan.DomainOld.Contacts;

public record ContactId(Ulid Value) : EntityId(Value);
// [StronglyTypedId]
// public partial struct ContactId
// {
// }