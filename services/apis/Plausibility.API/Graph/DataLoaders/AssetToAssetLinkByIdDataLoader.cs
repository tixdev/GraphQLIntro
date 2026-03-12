using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.DataLoaders;

public class AssetToAssetLinkByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PlausibilityDbContext dbContext)
    : BatchDataLoader<int, AssetToAssetLink>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, AssetToAssetLink>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.AssetToAssetLinks
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(p => keys.Contains(p.AssetToAssetLinkID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.AssetToAssetLinkID);
    }
}
