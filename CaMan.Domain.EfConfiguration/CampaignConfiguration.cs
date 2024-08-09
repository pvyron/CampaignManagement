using CaMan.Domain.Campaigns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaMan.Domain.EfConfiguration;

internal class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
{
    public void Configure(EntityTypeBuilder<Campaign> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Id)
            .HasConversion(p => p.Value.ToString(), value => new(Ulid.Parse(value)));

        builder.Property(c => c.CreatorId)
            .HasConversion(c => c.Value.ToString(), value => new(Ulid.Parse(value)));
        
        builder.Property(c => c.Title)
            .HasConversion(c => c.Value, value => CampaignTitle.Create(value));

        builder.Property(c => c.Description)
            .HasConversion(c => c.Value, value => CampaignDescription.Create(value));

        builder.HasMany(c => c.CampaignContacts);
    }
}