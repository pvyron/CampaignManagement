using CaMan.Domain.Shared;

namespace CaMan.Domain.Campaigns;

public record CampaignContactId(Ulid Value) : EntityId(Value);