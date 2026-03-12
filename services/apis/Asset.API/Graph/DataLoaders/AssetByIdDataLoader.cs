using Microsoft.EntityFrameworkCore;
using Asset.API.Data;
using AssetModel = Asset.API.Models.Asset;

namespace Asset.API.Graph.DataLoaders;

public class AssetByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    AssetContext dbContext)
    : BatchDataLoader<int, AssetModel>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, AssetModel>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.Asset
            .Where(r => keys.Contains(r.AssetID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(r => r.AssetID);
    }
}
