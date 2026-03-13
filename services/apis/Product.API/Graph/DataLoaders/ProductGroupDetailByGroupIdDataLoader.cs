using Microsoft.EntityFrameworkCore;
using Product.API.Data;
using Product.API.Models;

namespace Product.API.Graph.DataLoaders;

public class ProductGroupDetailByGroupIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    ProductDbContext dbContext)
    : GroupedDataLoader<int, ProductGroupDetail>(batchScheduler, options)
{
    protected override async Task<ILookup<int, ProductGroupDetail>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var results = await dbContext.ProductGroupDetail
            .AsNoTracking()
            .Where(t => keys.Contains(t.ProductGroupId))
            .ToListAsync(cancellationToken);

        return results.ToLookup(t => t.ProductGroupId);
    }
}
