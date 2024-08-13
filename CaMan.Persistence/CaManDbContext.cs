using CaMan.Domain.Campaigns;
using CaMan.Domain.Contacts;
using CaMan.Domain.EfConfiguration;
using CaMan.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CaMan.Persistence;

public sealed class CaManDbContext : DbContext
{
    public CaManDbContext(DbContextOptions<CaManDbContext> options) : base(options)
    {
        Users = Set<User>();
        Contacts = Set<Contact>();
        Campaigns = Set<Campaign>();
        CampaignContacts = Set<CampaignContact>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntryPointer).Assembly);
    }

    public DbSet<User> Users { get; }
    public DbSet<Contact> Contacts { get; }
    public DbSet<Campaign> Campaigns { get; }
    public DbSet<CampaignContact> CampaignContacts { get; }
}