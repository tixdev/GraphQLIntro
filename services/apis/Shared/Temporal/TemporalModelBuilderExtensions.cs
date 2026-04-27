using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Shared.Temporal;

public static class TemporalModelBuilderExtensions
{
    /// <summary>
    /// Applies temporal Global Query Filters to all entities in the model that implement 
    /// ITemporalEntity or ITemporalNullableEntity.
    /// This version captures the instance and is kept for backward compatibility, but it's 
    /// not recommended for HotChocolate 16 Eager Initialization.
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

    /// <summary>
    /// Applies temporal Global Query Filters dynamically using the DbContext instance.
    /// Required for HotChocolate 16 Eager Initialization.
    /// </summary>
    public static void ApplyTemporalFilters<TContext>(this ModelBuilder modelBuilder, TContext context)
        where TContext : DbContext, ITemporalDbContext
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ITemporalEntity).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(TemporalModelBuilderExtensions)
                    .GetMethod(nameof(ApplyTemporalFilterDynamic), BindingFlags.NonPublic | BindingFlags.Static)!
                    .MakeGenericMethod(entityType.ClrType, typeof(TContext));
                    
                method.Invoke(null, new object[] { modelBuilder, context });
            }
            else if (typeof(ITemporalNullableEntity).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(TemporalModelBuilderExtensions)
                    .GetMethod(nameof(ApplyNullableTemporalFilterDynamic), BindingFlags.NonPublic | BindingFlags.Static)!
                    .MakeGenericMethod(entityType.ClrType, typeof(TContext));
                    
                method.Invoke(null, new object[] { modelBuilder, context });
            }
        }
    }

    private static void ApplyTemporalFilter<TEntity>(ModelBuilder modelBuilder, ITemporalContext temporalContext) 
        where TEntity : class, ITemporalEntity
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter(e =>
            e.ValidStartDate <= temporalContext.QueryMaxStartDate &&
            e.ValidEndDate > temporalContext.QueryMinEndDate);
    }

    private static void ApplyTemporalFilterDynamic<TEntity, TContext>(ModelBuilder modelBuilder, TContext context) 
        where TEntity : class, ITemporalEntity
        where TContext : DbContext, ITemporalDbContext
    {
        // By using the 'context' parameter which is passed as 'this' from OnModelCreating,
        // EF Core will correctly treat this as a dynamic access to the current instance properties.
        modelBuilder.Entity<TEntity>().HasQueryFilter(e =>
            e.ValidStartDate <= context.TemporalContext.QueryMaxStartDate &&
            e.ValidEndDate > context.TemporalContext.QueryMinEndDate);
    }

    private static void ApplyNullableTemporalFilter<TEntity>(ModelBuilder modelBuilder, ITemporalContext temporalContext) 
        where TEntity : class, ITemporalNullableEntity
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter(e =>
            e.ValidStartDate <= temporalContext.QueryMaxStartDate &&
            (e.ValidEndDate ?? DateTime.MaxValue) > temporalContext.QueryMinEndDate);
    }

    private static void ApplyNullableTemporalFilterDynamic<TEntity, TContext>(ModelBuilder modelBuilder, TContext context) 
        where TEntity : class, ITemporalNullableEntity
        where TContext : DbContext, ITemporalDbContext
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter(e =>
            e.ValidStartDate <= context.TemporalContext.QueryMaxStartDate &&
            (e.ValidEndDate ?? DateTime.MaxValue) > context.TemporalContext.QueryMinEndDate);
    }
}
