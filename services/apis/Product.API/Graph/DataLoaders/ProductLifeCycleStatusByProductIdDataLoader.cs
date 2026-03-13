using Microsoft.EntityFrameworkCore;
using Product.API.Data;
using Product.API.Models;

namespace Product.API.Graph.DataLoaders;

public class ProductLifeCycleStatusByProductIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    ProductDbContext dbContext)
    : GroupedDataLoader<int, ProductLifeCycleStatus>(batchScheduler, options)
{
    protected override async Task<ILookup<int, ProductLifeCycleStatus>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var results = await dbContext.ProductLifeCycleStatus
            .AsNoTracking()
            .Where(t => keys.Contains(t.ProductId))
            .ToListAsync(cancellationToken);

        return results.ToLookup(t => t.ProductId);
    }
}
