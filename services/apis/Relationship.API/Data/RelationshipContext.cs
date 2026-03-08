using Microsoft.EntityFrameworkCore;
using Relationship.API.Models;
using Shared.Temporal;

namespace Relationship.API.Data;

public class RelationshipContext(DbContextOptions<RelationshipContext> options, ITemporalContext temporalContext) : DbContext(options)
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

        // Apply Global Query Filter for Temporal Entities dynamically
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ITemporalEntity).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(RelationshipContext).GetMethod(nameof(SetTemporalFilter), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (method != null)
                {
                    method.MakeGenericMethod(entityType.ClrType).Invoke(this, new object[] { modelBuilder });
                }
            }
        }
    }

    private void SetTemporalFilter<TEntity>(ModelBuilder modelBuilder) where TEntity : class, ITemporalEntity
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter(e =>
            temporalContext.Mode == TemporalFilterMode.All ||
            (temporalContext.Mode == TemporalFilterMode.AsOf 
                && e.ValidStartDate <= temporalContext.CurrentAsOfDate 
                && e.ValidEndDate >= temporalContext.CurrentAsOfDate) ||
            (temporalContext.Mode == TemporalFilterMode.ActiveBetween 
                && temporalContext.IsRangeStartProvided 
                && e.ValidStartDate <= temporalContext.SafeRangeEnd 
                && e.ValidEndDate >= temporalContext.SafeRangeStart)
        );
    }
}
