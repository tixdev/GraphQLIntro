using Microsoft.EntityFrameworkCore;
using Asset.API.Data;
using Asset.API.Models;

namespace Asset.API.Graph.DataLoaders;

public class AssetDebitCardsByAssetIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    AssetContext dbContext)
    : GroupedDataLoader<int, AssetDebitCard>(batchScheduler, options)
{
    protected override async Task<ILookup<int, AssetDebitCard>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.AssetDebitCard
            .Where(r => keys.Contains(r.AssetID))
            .ToListAsync(cancellationToken);

        return items.ToLookup(r => r.AssetID);
    }
}
