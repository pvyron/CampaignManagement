using CaMan.Domain.Campaigns;
using CaMan.Domain.Contacts;
using CaMan.Domain.Shared;

namespace CaMan.Domain.Users;

public sealed class User
{
    private User(ShortName shortName, Email email)
    {
        Id = new(Ulid.NewUlid());
        ShortName = shortName;
        Email = email;
    }

    public UserId Id { get; private set; }
    public ShortName ShortName { get; private set; }
    public Email Email { get; private set; }

    public Contact? ContactInfo { get; private set; }
    public Password? Password { get; private set; }

    private void CreateContactInfo()
    {
        ContactInfo = Contact.Create(ShortName, Email);
    }

    public void UpdateShortName(ShortName shortName)
    {
        ShortName = shortName;
    }

    public void UpdateEmail(Email email)
    {
        Email = email;
    }

    public Campaign CreateNewCampaign(CampaignTitle title, CampaignDescription description)
    {
        return Campaign.Create(this, title, description);
    }

    public static User Create(ShortName shortName, Email email)
    {
        var newUser = new User(shortName, email);

        newUser.CreateContactInfo();

        return newUser;
    }

    public bool Register(string password)
    {
        if (Password is not null)
            return false;

        Password = Password.Create(password);
        return true;
    }
}