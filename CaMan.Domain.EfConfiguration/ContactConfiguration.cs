using CaMan.Domain.Contacts;
using CaMan.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaMan.Domain.EfConfiguration;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(c => c.Value.ToString(), value => new(Ulid.Parse(value)));

        builder.Property(c => c.ShortName)
            .HasConversion(c => c.Value, value => ShortName.Create(value));

        builder.Property(c => c.FirstName)
            .HasConversion(c => c == null ? null : c.Value, value => string.IsNullOrWhiteSpace(value) ? null : FirstName.Create(value));

        builder.Property(c => c.LastName)
            .HasConversion(c => c == null ? null : c.Value, value => string.IsNullOrWhiteSpace(value) ? null : LastName.Create(value));

        builder.Property(c => c.Email)
            .HasConversion(c => c == null ? null : c.Value, value => string.IsNullOrWhiteSpace(value) ? null : Email.Create(value));

        builder.OwnsOne(c => c.PhoneNumber, phoneBuilder =>
        {
            phoneBuilder.Property(p => p.Phone).HasMaxLength(10);
        });
    }
}