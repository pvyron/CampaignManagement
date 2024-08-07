using CaMan.Domain.Contacts;

namespace CaMan.Domain.Campaigns;

public class CampaignContact
{
    private CampaignContact(CampaignId campaignId, ContactId contactId)
    {
        Id = new(Ulid.NewUlid());
        CampaignId = campaignId;
        ContactId = contactId;
    }
    
    public CampaignContactId Id { get; private set; }
    public CampaignId CampaignId { get; private set; }
    public ContactId ContactId { get; private set; }

    internal static CampaignContact Create(Campaign campaign, Contact contact)
    {
        return new(campaign.Id, contact.Id);
    }
}