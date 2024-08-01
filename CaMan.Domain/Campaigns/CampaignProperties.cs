namespace CaMan.Domain.Campaigns;

public record CampaignTitle
{
    private CampaignTitle(string value) => Value = value;
    
    public string Value { get; } = string.Empty;

    public static CampaignTitle Create(string value)
    {
        if (value.Length < 1)
        {
            throw new Exception("length invalid");
        }
        
        return new CampaignTitle(value);
    }
}

public record CampaignDescription
{
    private CampaignDescription(string value) => Value = value;
    
    public string Value { get; } = string.Empty;

    public static CampaignDescription Create(string value)
    {
        if (value.Length < 1)
        {
            throw new Exception("length invalid");
        }
        
        return new CampaignDescription(value);
    }
}