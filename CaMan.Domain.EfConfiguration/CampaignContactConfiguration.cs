using CaMan.Domain.Campaigns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaMan.Domain.EfConfiguration;

internal class CampaignContactConfiguration : IEntityTypeConfiguration<CampaignContact>
{
    public void Configure(EntityTypeBuilder<CampaignContact> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Id)
            .HasConversion(p => p.Value.ToString(), value => new(Ulid.Parse(value)));
        
        builder.Property(c => c.ContactId)
            .HasConversion(c => c.Value.ToString(), value => new(Ulid.Parse(value)));

        builder.Property(c => c.CampaignId)
            .HasConversion(c => c.Value.ToString(), value => new(Ulid.Parse(value)));
    }
}