using CaMan.DomainOld.Shared;

namespace CaMan.DomainOld.Campaigns;

public record CampaignContactId(Ulid Value) : EntityId(Value);