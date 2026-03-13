using Microsoft.EntityFrameworkCore;
using Product.API.Data;
using Product.API.Models;

namespace Product.API.Graph.DataLoaders;

public class ProductGroupToProductByProductIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    ProductDbContext dbContext)
    : GroupedDataLoader<int, ProductGroupToProduct>(batchScheduler, options)
{
    protected override async Task<ILookup<int, ProductGroupToProduct>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var results = await dbContext.ProductGroupToProduct
            .AsNoTracking()
            .Where(t => keys.Contains(t.ProductId))
            .ToListAsync(cancellationToken);

        return results.ToLookup(t => t.ProductId);
    }
}
