using DotNetTalk.AI.Profile.Finder.Gateways.Pg.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetTalk.AI.Profile.Finder.Gateways.Pg;

public class AiProfileFinderDbContext : DbContext
{
    public DbSet<PersonPersistence> People { get; set; }
    
    public DbSet<CompanyPersistence> Companies { get; set; }
    
    public DbSet<InsurancePersistence> Insurances { get; set; }
    
    public DbSet<PersonCompanyPersistence> PersonCompanies { get; set; }
    
    public DbSet<TravelPersistence> Travels { get; set; }

    public AiProfileFinderDbContext(DbContextOptions<AiProfileFinderDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonPersistence>().HasKey(e => e.Id);
        modelBuilder.Entity<CompanyPersistence>().HasKey(e => e.Id);
        modelBuilder.Entity<InsurancePersistence>().HasKey(e => e.Id);
        modelBuilder.Entity<PersonCompanyPersistence>().HasKey(e => e.Id);
        modelBuilder.Entity<TravelPersistence>().HasKey(e => e.Id);

        modelBuilder.Entity<PersonPersistence>()
            .HasMany(e => e.PersonCompanies)
            .WithOne(e => e.Person)
            .HasForeignKey("PersonId");

        modelBuilder.Entity<PersonPersistence>()
            .HasMany(e => e.Travels)
            .WithOne(e => e.Person)
            .HasForeignKey("PersonId");

        modelBuilder.Entity<PersonPersistence>()
            .HasMany(e => e.Insurances)
            .WithOne(e => e.Person)
            .HasForeignKey("PersonId");

        modelBuilder.Entity<CompanyPersistence>()
            .HasMany(e => e.PersonCompanies)
            .WithOne(e => e.Company)
            .HasForeignKey("CompanyId");

        modelBuilder.Entity<PersonPersistence>()
            .HasIndex(p => new { p.FirstName, p.LastName });
    }
}