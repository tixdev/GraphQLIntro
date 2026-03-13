using Microsoft.EntityFrameworkCore;
using Product.API.Data;
using Product.API.Models;

namespace Product.API.Graph.DataLoaders;

public class ProductByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    ProductDbContext dbContext)
    : BatchDataLoader<int, Models.Product>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, Models.Product>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        return await dbContext.Product
            .AsNoTracking()
            .Where(t => keys.Contains(t.ProductId))
            .ToDictionaryAsync(t => t.ProductId, cancellationToken);
    }
}
