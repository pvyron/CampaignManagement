namespace CaMan.Domain.Shared;

public record Email()
{
    private Email(string value) : this() => Value = value;
    
    public string Value { get; }

    public static Email Create(string value)
    {
        if (value.Length < 1) //email validation)
        {
            throw new Exception("email invalid");
        }
        
        return new Email(value);
    }
}