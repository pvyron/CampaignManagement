﻿using CaMan.Domain.Shared;
using CaMan.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaMan.Domain.EfConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion(id => id.Value.ToString(), value => new UserId(Ulid.Parse(value)));

        builder.Property(u => u.ShortName)
            .HasConversion(name => name.Value, value => ShortName.Create(value));
        
        builder.Property(u => u.Email)
            .HasConversion(email => email.Value, value => Email.Create(value));

        builder.HasOne(u => u.ContactInfo);
    }
}