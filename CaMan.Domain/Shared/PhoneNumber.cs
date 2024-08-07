namespace CaMan.Domain.Shared;

public record PhoneNumber
{
    private const ushort PhoneNumberLength = 10; 
    
    private PhoneNumber(string phone, string regionalPrefix)
    {
        if (phone.Length != PhoneNumberLength)
        {
            throw new($"length must be {PhoneNumberLength}");
        }

        if (string.IsNullOrWhiteSpace(regionalPrefix))
        {
            throw new("must not be empty");
        }
        
        Phone = phone;
        RegionalPrefix = regionalPrefix;
    }
    
    public string Phone { get; init; } 
    public string RegionalPrefix { get; init; }

    public static PhoneNumber Create(string phone) => new(phone, "0030");
    public static PhoneNumber Create(string phone, string regionalPrefix) => new(phone, regionalPrefix);
}