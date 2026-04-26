using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Shared.Temporal;

public static class TemporalModelBuilderExtensions
{
    /// <summary>
    /// Applies temporal Global Query Filters to all entities in the model that implement 
    /// ITemporalEntity or ITemporalNullableEntity.
    /// </summary>
    public static void ApplyTemporalFilters(this ModelBuilder modelBuilder, ITemporalContext temporalContext)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ITemporalEntity).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(TemporalModelBuilderExtensions)
                    .GetMethod(nameof(ApplyTemporalFilter), BindingFlags.NonPublic | BindingFlags.Static)!
                    .MakeGenericMethod(entityType.ClrType);
                    
                method.Invoke(null, new object[] { modelBuilder, temporalContext });
            }
            else if (typeof(ITemporalNullableEntity).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(TemporalModelBuilderExtensions)
                    .GetMethod(nameof(ApplyNullableTemporalFilter), BindingFlags.NonPublic | BindingFlags.Static)!
                    .MakeGenericMethod(entityType.ClrType);
                    
                method.Invoke(null, new object[] { modelBuilder, temporalContext });
            }
        }
    }

    private static void ApplyTemporalFilter<TEntity>(ModelBuilder modelBuilder, ITemporalContext temporalContext) 
        where TEntity : class, ITemporalEntity
    {
        // EF Core will parameterize these MemberAccess expressions at runtime
        modelBuilder.Entity<TEntity>().HasQueryFilter(e =>
            e.ValidStartDate <= temporalContext.QueryMaxStartDate &&
            e.ValidEndDate > temporalContext.QueryMinEndDate);
    }

    private static void ApplyNullableTemporalFilter<TEntity>(ModelBuilder modelBuilder, ITemporalContext temporalContext) 
        where TEntity : class, ITemporalNullableEntity
    {
        // The null-coalescing operator ?? transforms NULL into the "Magic Date" 
        // to maintain the universal mathematical logic without using OR operators.
        // EF Core safely translates this into a COALESCE or ISNULL in SQL.
        modelBuilder.Entity<TEntity>().HasQueryFilter(e =>
            e.ValidStartDate <= temporalContext.QueryMaxStartDate &&
            (e.ValidEndDate ?? DateTime.MaxValue) > temporalContext.QueryMinEndDate);
    }
}
