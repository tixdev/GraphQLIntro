using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.DataLoaders;

public class AssetToPersonLinkByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PlausibilityDbContext dbContext)
    : BatchDataLoader<int, AssetToPersonLink>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, AssetToPersonLink>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.AssetToPersonLinks
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(p => keys.Contains(p.AssetToPersonLinkID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.AssetToPersonLinkID);
    }
}
