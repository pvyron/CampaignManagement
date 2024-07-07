using CaMan.Domain.Contacts;

namespace CaMan.Domain.Campaigns;

public class CampaignContact
{
    internal CampaignContact(Guid id, CampaignId campaignId, ContactId contactId)
    {
        Id = id;
        CampaignId = campaignId;
        ContactId = contactId;
    }
    
    public Guid Id { get; private set; }
    public CampaignId CampaignId { get; private set; }
    public ContactId ContactId { get; private set; }
}