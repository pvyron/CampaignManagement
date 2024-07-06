namespace CaMan.Domain;

public sealed class Person
{
    private Person(string shortName, string firstName, string lastName, PersonRole role, PersonAgeGroup ageGroup, string? email, string? phoneNumber)
    {
        Id = Guid.NewGuid();
        
        ShortName = shortName;
        FirstName = firstName;
        LastName = lastName;
        Role = role;
        AgeGroup = ageGroup;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public Guid Id { get; private set; }
    public string ShortName { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? Email { get; private set; }
    public string? PhoneNumber { get; private set; }
    public PersonRole Role { get; private set; }
    public PersonAgeGroup AgeGroup { get; private set; }
    
    internal static Person CreateContact(string shortName, string firstName, string lastName, string? phoneNumber = null, string? email = null, PersonAgeGroup ageGroup = PersonAgeGroup.Uknown)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber) && string.IsNullOrWhiteSpace(email))
        {
            throw new Exception("phone or email is required for a contact");
        }
        
        return new(shortName, firstName, lastName, PersonRole.Contact, ageGroup, email, phoneNumber);
    }
    
    internal static Person CreateMotivator(string shortName, string firstName, string lastName, string phoneNumber, string? email = null, PersonAgeGroup ageGroup = PersonAgeGroup.Uknown)
    {
        return new(shortName, firstName, lastName, PersonRole.Motivator, ageGroup, email, phoneNumber);
    }
    
    internal static Person CreateOrganizer(string shortName, string firstName, string lastName, string phoneNumber, string email, PersonAgeGroup ageGroup = PersonAgeGroup.Uknown)
    {
        return new(shortName, firstName, lastName, PersonRole.Organizer, ageGroup, email, phoneNumber);
    }
    
    internal static Person CreateManager(string shortName, string firstName, string lastName, string phoneNumber, string email, PersonAgeGroup ageGroup = PersonAgeGroup.Uknown)
    {
        return new(shortName, firstName, lastName, PersonRole.Manager, ageGroup, email, phoneNumber);
    }
    
    public enum PersonAgeGroup
    {
        Uknown,
        Student,
        Youth,
        MiddleAged,
        Elder
    }

    public enum PersonRole
    {
        Contact,
        Motivator,
        Organizer,
        Manager
    }
}