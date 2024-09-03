using CaMan.DomainOld.Contacts;
using CaMan.DomainOld.Users;

namespace CaMan.DomainOld.Campaigns;

public sealed class Campaign
{
    private Campaign(UserId creatorId, CampaignTitle title, CampaignDescription description)
    {
        Id = new(Ulid.NewUlid());
        CreatorId = creatorId;
        Title = title;
        Description = description;
    }
    
    public UserId CreatorId { get; private set; }
    public CampaignId Id { get; private set; }
    public CampaignTitle Title { get; private set; }
    public CampaignDescription Description { get; private set; }

    private readonly HashSet<CampaignContact> _campaignContacts = [];
    public IReadOnlyCollection<CampaignContact> CampaignContacts => _campaignContacts;
    
    public void AddContact(Contact contact, IQueryable<CampaignContact> existingContacts)
    {
        if (existingContacts.Any(c => c.ContactId == contact.Id)) // questionable if this works
        {
            throw new("already exists");
        }
        
        var campaignContact = CampaignContact.Create(this, contact);

        _campaignContacts.Add(campaignContact);
    }

    internal static Campaign Create(User user, CampaignTitle title, CampaignDescription description)
    {
        return new(user.Id, title, description);
    }
}