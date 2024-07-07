namespace CaMan.Domain.Contacts;

public record PhoneNumber()
{
    private const ushort PhoneNumberLength = 10; 
    
    private PhoneNumber(string phone, string regionalPrefix) : this()
    {
        if (phone.Length != PhoneNumberLength)
        {
            throw new Exception($"length must be {PhoneNumberLength}");
        }

        if (string.IsNullOrWhiteSpace(regionalPrefix))
        {
            throw new Exception("must not be empty");
        }
        
        Phone = phone;
        RegionalPrefix = regionalPrefix;
    }
    
    public string Phone { get; init; } 
    public string RegionalPrefix { get; init; }

    public static PhoneNumber Create(string phone) => new PhoneNumber(phone, "0030");
    public static PhoneNumber Create(string phone, string regionalPrefix) => new PhoneNumber(phone, regionalPrefix);
}