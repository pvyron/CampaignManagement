namespace CaMan.Domain.Contacts;

public class Contact
{
    public ContactId Id { get; private set; }
    public string ShortName { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public Email? Email { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public ContactCommunicationMethod CommunicationMethod { get; private set; }
    public ContactAgeGroup AgeGroup { get; private set; }
}