using CaMan.Domain.Contacts;

namespace CaMan.Domain.Campaigns;

public class CampaignContact
{
    internal CampaignContact(CampaignContactId id, CampaignId campaignId, ContactId contactId)
    {
        Id = id;
        CampaignId = campaignId;
        ContactId = contactId;
    }
    
    public CampaignContactId Id { get; private set; }
    public CampaignId CampaignId { get; private set; }
    public ContactId ContactId { get; private set; }
}