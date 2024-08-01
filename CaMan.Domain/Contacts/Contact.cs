using CaMan.Domain.Shared;

namespace CaMan.Domain.Contacts;

public sealed class Contact
{
    private Contact(ShortName shortName, FirstName? firstName, LastName? lastName, Email? email, PhoneNumber? phoneNumber, ContactCommunicationMethod communicationMethod, ContactAgeGroup ageGroup)
    {
        Id = new ContactId(Ulid.NewUlid());
        ShortName = shortName;
        FirstName = firstName;
        LastName = lastName;
        
        CommunicationMethod = communicationMethod;
        PhoneNumber = phoneNumber;
        Email = email;

        switch (communicationMethod)
        {
            case ContactCommunicationMethod.Phone:
                if (string.IsNullOrWhiteSpace(phoneNumber?.Phone))
                    throw new Exception("cant have empty phone");
                break;
            case ContactCommunicationMethod.Email:
                if (string.IsNullOrWhiteSpace(email?.Value))
                    throw new Exception("cant have empty phone");
                break;
            case ContactCommunicationMethod.Phone_And_Email:
                if (string.IsNullOrWhiteSpace(email?.Value))
                    throw new Exception("cant have empty phone");
                if (string.IsNullOrWhiteSpace(phoneNumber?.Phone))
                    throw new Exception("cant have empty phone");
                break;
        }
        
        AgeGroup = ageGroup;
    }
    
    public ContactId Id { get; private set; }
    public ShortName ShortName { get; private set; }
    public FirstName? FirstName { get; private set; }
    public LastName? LastName { get; private set; }
    public Email? Email { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public ContactCommunicationMethod CommunicationMethod { get; private set; }
    public ContactAgeGroup AgeGroup { get; private set; }

    internal static Contact Create(ShortName shortName, PhoneNumber phoneNumber, FirstName? firstName = null, LastName? lastName = null, ContactAgeGroup ageGroup = ContactAgeGroup.Uknown)
    {
        return new Contact(shortName, firstName, lastName, null, phoneNumber, ContactCommunicationMethod.Phone,
            ageGroup);
    }

    internal static Contact Create(ShortName shortName, Email email, FirstName? firstName = null, LastName? lastName = null, ContactAgeGroup ageGroup = ContactAgeGroup.Uknown)
    {
        return new Contact(shortName, firstName, lastName, email, null, ContactCommunicationMethod.Email,
            ageGroup);
    }

    internal static Contact Create(ShortName shortName, PhoneNumber phoneNumber, Email email, FirstName? firstName = null, LastName? lastName = null, ContactAgeGroup ageGroup = ContactAgeGroup.Uknown)
    {
        return new Contact(shortName, firstName, lastName, email, phoneNumber, ContactCommunicationMethod.Phone_And_Email,
            ageGroup);
    }
}