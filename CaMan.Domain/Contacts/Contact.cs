using CaMan.Domain.ElectoralBodies;
using CaMan.Domain.Shared;

namespace CaMan.Domain.Contacts;

public sealed class Contact
{
    private Contact()
    {
        
    }
    private Contact(ShortName shortName, FirstName? firstName, LastName? lastName, Email? email, PhoneNumber? phoneNumber, ContactCommunicationMethod communicationMethod, ContactAgeGroup ageGroup)
    {
        Id = new(Ulid.NewUlid());
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
                    throw new("cant have empty phone");
                break;
            case ContactCommunicationMethod.Email:
                if (string.IsNullOrWhiteSpace(email?.Value))
                    throw new("cant have empty phone");
                break;
            case ContactCommunicationMethod.Phone_And_Email:
                if (string.IsNullOrWhiteSpace(email?.Value))
                    throw new("cant have empty phone");
                if (string.IsNullOrWhiteSpace(phoneNumber?.Phone))
                    throw new("cant have empty phone");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(communicationMethod), communicationMethod, null);
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
    
    public AdministrativeRegion? AdministrativeRegion { get; private set; }
    public RegionalUnit? RegionalUnit { get; private set; }
    public Municipality? Municipality { get; private set; }
    public MunicipalUnit? MunicipalUnit { get; private set; }

    public void UpdateElectoralRegion(AdministrativeRegion administrativeRegion) => AdministrativeRegion = administrativeRegion;
    public void UpdateElectoralRegion(RegionalUnit regionalUnit) => RegionalUnit = regionalUnit;
    public void UpdateElectoralRegion(Municipality municipality) => Municipality = municipality;
    public void UpdateElectoralRegion(MunicipalUnit municipalUnit) => MunicipalUnit = municipalUnit;

    internal static Contact Create(ShortName shortName, PhoneNumber phoneNumber, FirstName? firstName = null, LastName? lastName = null, ContactAgeGroup ageGroup = ContactAgeGroup.Uknown)
    {
        return new(shortName, firstName, lastName, null, phoneNumber, ContactCommunicationMethod.Phone,
            ageGroup);
    }

    internal static Contact Create(ShortName shortName, Email email, FirstName? firstName = null, LastName? lastName = null, ContactAgeGroup ageGroup = ContactAgeGroup.Uknown)
    {
        return new(shortName, firstName, lastName, email, null, ContactCommunicationMethod.Email,
            ageGroup);
    }

    internal static Contact Create(ShortName shortName, PhoneNumber phoneNumber, Email email, FirstName? firstName = null, LastName? lastName = null, ContactAgeGroup ageGroup = ContactAgeGroup.Uknown)
    {
        return new(shortName, firstName, lastName, email, phoneNumber, ContactCommunicationMethod.Phone_And_Email,
            ageGroup);
    }
}