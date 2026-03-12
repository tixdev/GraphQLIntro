using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.DataLoaders;

public class AssetStatusReasonByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PlausibilityDbContext dbContext)
    : BatchDataLoader<int, AssetStatusReason>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, AssetStatusReason>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.AssetStatusReasons
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(p => keys.Contains(p.AssetStatusReasonID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.AssetStatusReasonID);
    }
}
