using Microsoft.EntityFrameworkCore;
using Relationship.API.Models;
using Shared.Temporal;

namespace Relationship.API.Data;

public class RelationshipContext(DbContextOptions<RelationshipContext> options) : DbContext(options)
{
    public DbSet<Models.Relationship> Relationships { get; set; }
    public DbSet<RelationshipToPerson> RelationshipToPersons { get; set; }
    public DbSet<RelationshipName> RelationshipNames { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Relationship>(entity =>
        {
            entity.HasKey(e => e.RelationshipID);
            
            entity.HasOne(e => e.Name)
                .WithOne(e => e.Relationship)
                .HasForeignKey<RelationshipName>(e => e.RelationshipID);
        });

        modelBuilder.Entity<RelationshipToPerson>(entity =>
        {
            entity.HasKey(e => e.RelationshipToPersonID);
            
            entity.HasOne(e => e.Relationship)
                .WithMany(e => e.RelationshipToPersons)
                .HasForeignKey(e => e.RelationshipID);
        });

        modelBuilder.Entity<RelationshipName>(entity =>
        {
            entity.HasKey(e => e.RelationshipID);
        });
    }
}

