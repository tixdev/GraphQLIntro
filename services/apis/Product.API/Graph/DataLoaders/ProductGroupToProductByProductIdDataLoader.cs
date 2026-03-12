using Microsoft.EntityFrameworkCore;
using Product.API.Data;
using Product.API.Models;

namespace Product.API.Graph.DataLoaders;

public class ProductGroupToProductByProductIdDataLoader : GroupedDataLoader<int, ProductGroupToProduct>
{
    private readonly IDbContextFactory<ProductDbContext> _dbContextFactory;

    public ProductGroupToProductByProductIdDataLoader(
        IDbContextFactory<ProductDbContext> dbContextFactory,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null)
        : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task<ILookup<int, ProductGroupToProduct>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var results = await dbContext.ProductGroupToProduct
            .AsNoTracking()
            .Where(t => keys.Contains(t.ProductId))
            .ToListAsync(cancellationToken);

        return results.ToLookup(t => t.ProductId);
    }
}
