using CaMan.Domain.Contacts;

namespace CaMan.Domain.Campaigns;

public sealed class Campaign
{
    private Campaign(CampaignTitle title, CampaignDescription description)
    {
        Id = new CampaignId(Ulid.NewUlid());
        Title = title;
        Description = description;
    }
    
    public CampaignId Id { get; private set; }
    public CampaignTitle Title { get; private set; }
    public CampaignDescription Description { get; private set; }

    private readonly HashSet<CampaignContact> _campaignContacts = [];
    public IReadOnlyCollection<CampaignContact> CampaignContacts => _campaignContacts;
    
    public void AddContact(Contact contact)
    {
        var campaignContact = new CampaignContact(new CampaignContactId(Ulid.NewUlid()), Id, contact.Id);

        _campaignContacts.Add(campaignContact);
    }

    public static Campaign Create(CampaignTitle title, CampaignDescription description)
    {
        return new Campaign(title, description);
    }
}