using Microsoft.EntityFrameworkCore;
using Asset.API.Data;
using Asset.API.Models;

namespace Asset.API.Graph.DataLoaders;

public class AssetIntermediariesByAssetIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    AssetContext dbContext)
    : GroupedDataLoader<int, AssetIntermediary>(batchScheduler, options)
{
    protected override async Task<ILookup<int, AssetIntermediary>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.AssetIntermediary
            .Where(r => keys.Contains(r.AssetID))
            .ToListAsync(cancellationToken);

        return items.ToLookup(r => r.AssetID);
    }
}
