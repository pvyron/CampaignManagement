using CaMan.Domain.Contacts;

namespace CaMan.Domain.Campaigns;

public class Campaign
{
    public CampaignId Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    private readonly HashSet<CampaignContact> _campaignContacts = new();
    public IReadOnlyCollection<CampaignContact> CampaignContacts => _campaignContacts;

    public void AddContact(Contact contact)
    {
        var campaignContact = new CampaignContact(new CampaignContactId(Ulid.NewUlid()), Id, contact.Id);

        _campaignContacts.Add(campaignContact);
    }
}