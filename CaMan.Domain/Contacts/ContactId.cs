using CaMan.Domain.Shared;

namespace CaMan.Domain.Contacts;

public record ContactId(Ulid Value) : EntityId(Value);
// [StronglyTypedId]
// public partial struct ContactId
// {
// }