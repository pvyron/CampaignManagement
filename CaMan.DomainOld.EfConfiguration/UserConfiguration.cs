using CaMan.DomainOld.Shared;
using CaMan.DomainOld.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaMan.DomainOld.EfConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .HasConversion(id => id.Value.ToString(), value => new UserId(Ulid.Parse(value)));
        
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(u => u.Email)
            .HasConversion(email => email.Value, value => Email.Create(value));

        builder.Property(u => u.ShortName)
            .HasConversion(name => name.Value, value => ShortName.Create(value));

        builder.HasOne(u => u.ContactInfo);

        var passwordBuilder = builder.OwnsOne(u => u.Password);
        passwordBuilder.Property(p => p.Hash)
            .HasConversion(h => Convert.ToHexString(h), v => Convert.FromHexString(v));
        passwordBuilder.Property(p => p.Salt)
            .HasConversion(s => Convert.ToHexString(s), v => Convert.FromHexString(v));
    }
}