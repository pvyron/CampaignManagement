using CaMan.DomainOld.Shared;

namespace CaMan.DomainOld.Campaigns;

public record CampaignId(Ulid Value) : EntityId(Value);
// [StronglyTypedId]
// public partial struct CampaignId
// {
// }