using Microsoft.EntityFrameworkCore;
using Product.API.Data;
using Product.API.Models;

namespace Product.API.Graph.DataLoaders;

public class ProductDocumentByProductIdDataLoader : GroupedDataLoader<int, ProductDocument>
{
    private readonly IDbContextFactory<ProductDbContext> _dbContextFactory;

    public ProductDocumentByProductIdDataLoader(
        IDbContextFactory<ProductDbContext> dbContextFactory,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null)
        : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task<ILookup<int, ProductDocument>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var results = await dbContext.ProductDocument
            .AsNoTracking()
            .Where(t => keys.Contains(t.ProductId))
            .ToListAsync(cancellationToken);

        return results.ToLookup(t => t.ProductId);
    }
}
