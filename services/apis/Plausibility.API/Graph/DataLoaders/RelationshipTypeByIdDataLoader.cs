using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.DataLoaders;

public class RelationshipTypeByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PlausibilityDbContext dbContext)
    : BatchDataLoader<int, RelationshipType>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, RelationshipType>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.RelationshipTypes
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(p => keys.Contains(p.RelationshipTypeID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.RelationshipTypeID);
    }
}
