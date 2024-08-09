using EmailValidation;

namespace CaMan.Domain.Shared;

public record Email
{
    private Email(string value) => Value = value;
    
    public string Value { get; }

    public static Email Create(string value)
    {
        if (!EmailValidator.TryValidate(value, true, false, out var error))
        {
            throw new Exception(error!.Code.ToString());
        }
        
        return new Email(value);
    }
}