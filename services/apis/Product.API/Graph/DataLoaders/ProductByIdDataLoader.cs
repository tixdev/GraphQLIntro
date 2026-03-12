using Microsoft.EntityFrameworkCore;
using Product.API.Data;
using Product.API.Models;

namespace Product.API.Graph.DataLoaders;

public class ProductByIdDataLoader : BatchDataLoader<int, Models.Product>
{
    private readonly IDbContextFactory<ProductDbContext> _dbContextFactory;

    public ProductByIdDataLoader(
        IDbContextFactory<ProductDbContext> dbContextFactory,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null)
        : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task<IReadOnlyDictionary<int, Models.Product>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await dbContext.Product
            .AsNoTracking()
            .Where(t => keys.Contains(t.ProductId))
            .ToDictionaryAsync(t => t.ProductId, cancellationToken);
    }
}
