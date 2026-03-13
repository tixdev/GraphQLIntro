using Microsoft.EntityFrameworkCore;
using Product.API.Data;
using Product.API.Models;

namespace Product.API.Graph.DataLoaders;

public class ProductGroupLifeCycleStatusByGroupIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    ProductDbContext dbContext)
    : GroupedDataLoader<int, ProductGroupLifeCycleStatus>(batchScheduler, options)
{
    protected override async Task<ILookup<int, ProductGroupLifeCycleStatus>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var results = await dbContext.ProductGroupLifeCycleStatus
            .AsNoTracking()
            .Where(t => keys.Contains(t.ProductGroupId))
            .ToListAsync(cancellationToken);

        return results.ToLookup(t => t.ProductGroupId);
    }
}
