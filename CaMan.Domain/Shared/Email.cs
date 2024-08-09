using System.Text.RegularExpressions;

namespace CaMan.Domain.Shared;

public record Email
{
    private Email(string value) => Value = value;
    
    public string Value { get; }

    public static Email Create(string value)
    {
        if (Validate(value)) return new Email(value);
        
        throw new Exception("Email validation failed");
    }

    private static readonly Regex EmailMatchingRegex = 
        new("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", 
        RegexOptions.Compiled | 
            RegexOptions.IgnoreCase | 
            RegexOptions.Singleline | 
            RegexOptions.NonBacktracking);

    static bool Validate(string value) => EmailMatchingRegex.IsMatch(value);
}