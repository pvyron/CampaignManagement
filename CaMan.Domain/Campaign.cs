namespace CaMan.Domain;

public sealed class Campaign
{
    private Campaign(string title, string? description, Person manager)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Manager = manager;
    }

    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    
    public Person Manager { get; private set; }

    private List<Person> _organizers = new();
    public IReadOnlyCollection<Person> Organizers => _organizers;

    private List<Person> _motivators = new();
    public IReadOnlyCollection<Person> Motivators => _motivators;

    private List<Person> _contacts = new();
    public IReadOnlyCollection<Person> Contacts => _contacts;

    public static Campaign Create(string title, Person manager, string? description = null)
    {
        return new(title, description, manager);
    }
    
    public Person AddOrganizer(string shortName, string firstName, string lastName, string phoneNumber, string email)
    {
        var organizer = Person.CreateOrganizer(shortName, firstName, lastName, phoneNumber, email);
        
        _organizers.Add(organizer);

        return organizer;
    }

    public Person AddMotivator(string shortName, string phoneNumber,
        string? email = null, string? firstName = null, string? lastName = null)
    {
        var motivator = Person.CreateMotivator(shortName, firstName ?? string.Empty, lastName ?? string.Empty, phoneNumber, email);
        
        _motivators.Add(motivator);

        return motivator;
    }

    public Person AddContact(string shortName, string? phoneNumber = null,
        string? email = null, string? firstName = null, string? lastName = null, Person.PersonAgeGroup ageGroup = Person.PersonAgeGroup.Uknown)
    {
        var contact = Person.CreateContact(shortName, firstName ?? string.Empty, lastName ?? string.Empty, phoneNumber, email, ageGroup);
        
        _contacts.Add(contact);

        return contact;
    }
}