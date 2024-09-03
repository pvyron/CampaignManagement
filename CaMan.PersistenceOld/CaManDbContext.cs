using CaMan.DomainOld.Campaigns;
using CaMan.DomainOld.Contacts;
using CaMan.DomainOld.EfConfiguration;
using CaMan.DomainOld.ElectoralBodies;
using CaMan.DomainOld.Users;
using Microsoft.EntityFrameworkCore;

namespace CaMan.PersistenceOld;

public sealed class CaManDbContext : DbContext
{
    public CaManDbContext(DbContextOptions<CaManDbContext> options) : base(options)
    {
        Users = Set<User>();
        Contacts = Set<Contact>();
        Campaigns = Set<Campaign>();
        CampaignContacts = Set<CampaignContact>();
        AdministrativeRegions = Set<AdministrativeRegion>();
        RegionalUnits = Set<RegionalUnit>();
        Municipalities = Set<Municipality>();
        MunicipalUnits = Set<MunicipalUnit>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntryPointer).Assembly);
    }

    public DbSet<User> Users { get; }
    public DbSet<Contact> Contacts { get; }
    public DbSet<Campaign> Campaigns { get; }
    public DbSet<CampaignContact> CampaignContacts { get; }
    public DbSet<AdministrativeRegion> AdministrativeRegions { get; }
    public DbSet<RegionalUnit> RegionalUnits { get; }
    public DbSet<Municipality> Municipalities { get; }
    public DbSet<MunicipalUnit> MunicipalUnits { get; }
}