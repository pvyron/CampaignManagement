﻿namespace CaMan.Domain.Campaigns;

public record struct CampaignDescription
{
    private CampaignDescription(string value) => Value = value;
    
    public string Value { get; } = string.Empty;

    public static CampaignDescription Create(string value)
    {
        if (value.Length < 1)
        {
            throw new("length invalid");
        }
        
        return new(value);
    }
}