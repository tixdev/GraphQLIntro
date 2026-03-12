using Microsoft.EntityFrameworkCore;
using Asset.API.Data;
using Asset.API.Models;

namespace Asset.API.Graph.DataLoaders;

public class AssetDetailsByAssetIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    AssetContext dbContext)
    : GroupedDataLoader<int, AssetDetail>(batchScheduler, options)
{
    protected override async Task<ILookup<int, AssetDetail>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.AssetDetail
            .Where(r => keys.Contains(r.AssetID))
            .ToListAsync(cancellationToken);

        return items.ToLookup(r => r.AssetID);
    }
}
