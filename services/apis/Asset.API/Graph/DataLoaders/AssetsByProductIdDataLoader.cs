using Microsoft.EntityFrameworkCore;
using Asset.API.Data;
using AssetModel = Asset.API.Models.Asset;

namespace Asset.API.Graph.DataLoaders;

public class AssetsByProductIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    AssetContext dbContext)
    : GroupedDataLoader<int, AssetModel>(batchScheduler, options)
{
    protected override async Task<ILookup<int, AssetModel>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.Asset
            .Where(r => keys.Contains(r.ProductID))
            .ToListAsync(cancellationToken);

        return items.ToLookup(r => r.ProductID);
    }
}
