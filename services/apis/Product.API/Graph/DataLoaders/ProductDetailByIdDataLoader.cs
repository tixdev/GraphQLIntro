using Microsoft.EntityFrameworkCore;
using Product.API.Data;
using Product.API.Models;

namespace Product.API.Graph.DataLoaders;

public class ProductDetailByIdDataLoader : BatchDataLoader<int, ProductDetail>
{
    private readonly IDbContextFactory<ProductDbContext> _dbContextFactory;

    public ProductDetailByIdDataLoader(
        IDbContextFactory<ProductDbContext> dbContextFactory,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null)
        : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task<IReadOnlyDictionary<int, ProductDetail>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await dbContext.ProductDetail
            .AsNoTracking()
            .Where(t => keys.Contains(t.ProductDetailId))
            .ToDictionaryAsync(t => t.ProductDetailId, cancellationToken);
    }
}
