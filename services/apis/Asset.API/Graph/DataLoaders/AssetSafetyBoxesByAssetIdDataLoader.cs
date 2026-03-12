using Microsoft.EntityFrameworkCore;
using Asset.API.Data;
using Asset.API.Models;

namespace Asset.API.Graph.DataLoaders;

public class AssetSafetyBoxesByAssetIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    AssetContext dbContext)
    : GroupedDataLoader<int, AssetSafetyBox>(batchScheduler, options)
{
    protected override async Task<ILookup<int, AssetSafetyBox>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.AssetSafetyBox
            .Where(r => keys.Contains(r.AssetID))
            .ToListAsync(cancellationToken);

        return items.ToLookup(r => r.AssetID);
    }
}
