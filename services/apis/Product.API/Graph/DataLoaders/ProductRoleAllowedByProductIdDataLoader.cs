using Microsoft.EntityFrameworkCore;
using Product.API.Data;
using Product.API.Models;

namespace Product.API.Graph.DataLoaders;

public class ProductRoleAllowedByProductIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    ProductDbContext dbContext)
    : GroupedDataLoader<int, ProductRoleAllowed>(batchScheduler, options)
{
    protected override async Task<ILookup<int, ProductRoleAllowed>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var results = await dbContext.ProductRoleAllowed
            .AsNoTracking()
            .Where(t => keys.Contains(t.ProductId))
            .ToListAsync(cancellationToken);

        return results.ToLookup(t => t.ProductId);
    }
}
