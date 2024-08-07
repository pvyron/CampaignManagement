namespace CaMan.Domain.Shared;

public record ShortName
{
    private ShortName(string value) => Value = value;
    
    public string Value { get; } = string.Empty;

    public static ShortName Create(string value)
    {
        if (value.Length < 1)
        {
            throw new("length invalid");
        }
        
        return new(value);
    }
}

public record FullName
{
    private FullName(string value) => Value = value;
    
    public string Value { get; } = string.Empty;

    public static FullName Create(string value)
    {
        if (value.Length < 1)
        {
            throw new("length invalid");
        }
        
        return new(value);
    }
}

public record FirstName
{
    private FirstName(string value) => Value = value;

    public string Value { get; } = string.Empty;

    public static FirstName Create(string value)
    {
        if (value.Length < 1)
        {
            throw new("length invalid");
        }
        
        return new(value);
    }
}

public record LastName
{
    private LastName(string value) => Value = value;
    
    public string Value { get; } = string.Empty;

    public static LastName Create(string value)
    {
        if (value.Length < 1)
        {
            throw new("length invalid");
        }
        
        return new(value);
    }
}