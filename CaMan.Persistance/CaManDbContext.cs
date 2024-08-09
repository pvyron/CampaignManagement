using CaMan.Domain.EfConfiguration;
using CaMan.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CaMan.Persistance;

public sealed class CaManDbContext : DbContext
{
    public CaManDbContext(DbContextOptions<CaManDbContext> options) : base(options)
    {
        Users = Set<User>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntryPointer).Assembly);
    }

    public DbSet<User> Users { get; }
}