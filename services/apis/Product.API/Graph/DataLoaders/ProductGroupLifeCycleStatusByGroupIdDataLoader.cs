using Microsoft.EntityFrameworkCore;
using Product.API.Data;
using Product.API.Models;

namespace Product.API.Graph.DataLoaders;

public class ProductGroupLifeCycleStatusByGroupIdDataLoader : GroupedDataLoader<int, ProductGroupLifeCycleStatus>
{
    private readonly IDbContextFactory<ProductDbContext> _dbContextFactory;

    public ProductGroupLifeCycleStatusByGroupIdDataLoader(
        IDbContextFactory<ProductDbContext> dbContextFactory,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null)
        : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task<ILookup<int, ProductGroupLifeCycleStatus>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var results = await dbContext.ProductGroupLifeCycleStatus
            .AsNoTracking()
            .Where(t => keys.Contains(t.ProductGroupId))
            .ToListAsync(cancellationToken);

        return results.ToLookup(t => t.ProductGroupId);
    }
}
