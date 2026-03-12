using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.DataLoaders;

public class AssetToRelationshipLinkByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PlausibilityDbContext dbContext)
    : BatchDataLoader<int, AssetToRelationshipLink>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, AssetToRelationshipLink>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.AssetToRelationshipLinks
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(p => keys.Contains(p.AssetToRelationshipLinkID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.AssetToRelationshipLinkID);
    }
}
