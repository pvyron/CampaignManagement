namespace CaMan.DomainOld.Campaigns;

public record struct CampaignTitle
{
    private CampaignTitle(string value) => Value = value;
    
    public string Value { get; } = string.Empty;

    public static CampaignTitle Create(string value)
    {
        if (value.Length < 1)
        {
            throw new("length invalid");
        }
        
        return new(value);
    }
}