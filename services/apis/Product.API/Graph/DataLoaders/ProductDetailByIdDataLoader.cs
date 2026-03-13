using Microsoft.EntityFrameworkCore;
using Product.API.Data;
using Product.API.Models;

namespace Product.API.Graph.DataLoaders;

public class ProductDetailByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    ProductDbContext dbContext)
    : BatchDataLoader<int, ProductDetail>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, ProductDetail>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        return await dbContext.ProductDetail
            .AsNoTracking()
            .Where(t => keys.Contains(t.ProductDetailId))
            .ToDictionaryAsync(t => t.ProductDetailId, cancellationToken);
    }
}
