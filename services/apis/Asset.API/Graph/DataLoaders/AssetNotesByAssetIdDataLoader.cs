using Microsoft.EntityFrameworkCore;
using Asset.API.Data;
using Asset.API.Models;

namespace Asset.API.Graph.DataLoaders;

public class AssetNotesByAssetIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    AssetContext dbContext)
    : GroupedDataLoader<int, AssetNote>(batchScheduler, options)
{
    protected override async Task<ILookup<int, AssetNote>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.AssetNote
            .Where(r => keys.Contains(r.AssetID))
            .ToListAsync(cancellationToken);

        return items.ToLookup(r => r.AssetID);
    }
}
