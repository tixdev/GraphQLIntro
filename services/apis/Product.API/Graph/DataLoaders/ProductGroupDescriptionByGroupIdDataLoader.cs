using Microsoft.EntityFrameworkCore;
using Product.API.Data;
using Product.API.Models;

namespace Product.API.Graph.DataLoaders;

public class ProductGroupDescriptionByGroupIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    ProductDbContext dbContext)
    : GroupedDataLoader<int, ProductGroupDescription>(batchScheduler, options)
{
    protected override async Task<ILookup<int, ProductGroupDescription>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var results = await dbContext.ProductGroupDescription
            .AsNoTracking()
            .Where(t => keys.Contains(t.ProductGroupId))
            .ToListAsync(cancellationToken);

        return results.ToLookup(t => t.ProductGroupId);
    }
}
