using CaMan.Domain.Shared;

namespace CaMan.Domain.Campaigns;

public record CampaignId(Ulid Value) : EntityId(Value);
// [StronglyTypedId]
// public partial struct CampaignId
// {
// }