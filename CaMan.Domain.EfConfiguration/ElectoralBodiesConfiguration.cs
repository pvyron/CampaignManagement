using CaMan.Domain.ElectoralBodies;
using CaMan.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaMan.Domain.EfConfiguration;

internal class AdministrativeRegionConfiguration : IEntityTypeConfiguration<AdministrativeRegion>
{
    public void Configure(EntityTypeBuilder<AdministrativeRegion> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(c => c.Value.ToByteArray(), value => new(Ulid.Parse(value)));

        builder.Property(c => c.ShortName)
            .HasConversion(c => c.Value, value => ShortName.Create(value))
            .HasMaxLength(100);

        builder.Property(c => c.FullName)
            .HasConversion(c => c.Value, value => FullName.Create(value))
            .HasMaxLength(100);

    }
}

internal class RegionalUnitConfiguration : IEntityTypeConfiguration<RegionalUnit>
{
    public void Configure(EntityTypeBuilder<RegionalUnit> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(c => c.Value.ToByteArray(), value => new(Ulid.Parse(value)));

        builder.Property(c => c.ShortName)
            .HasConversion(c => c.Value, value => ShortName.Create(value))
            .HasMaxLength(100);

        builder.Property(c => c.FullName)
            .HasConversion(c => c.Value, value => FullName.Create(value))
            .HasMaxLength(100);

    }
}

internal class MunicipalityConfiguration : IEntityTypeConfiguration<Municipality>
{
    public void Configure(EntityTypeBuilder<Municipality> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(c => c.Value.ToByteArray(), value => new(Ulid.Parse(value)));

        builder.Property(c => c.ShortName)
            .HasConversion(c => c.Value, value => ShortName.Create(value))
            .HasMaxLength(100);

        builder.Property(c => c.FullName)
            .HasConversion(c => c.Value, value => FullName.Create(value))
            .HasMaxLength(100);

    }
}

internal class MunicipalUnitConfiguration : IEntityTypeConfiguration<MunicipalUnit>
{
    public void Configure(EntityTypeBuilder<MunicipalUnit> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(c => c.Value.ToByteArray(), value => new(Ulid.Parse(value)));

        builder.Property(c => c.ShortName)
            .HasConversion(c => c.Value, value => ShortName.Create(value))
            .HasMaxLength(100);

        builder.Property(c => c.FullName)
            .HasConversion(c => c.Value, value => FullName.Create(value))
            .HasMaxLength(100);

    }
}