using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.DataLoaders;

public class PersonToRelationshipRoleByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PlausibilityDbContext dbContext)
    : BatchDataLoader<int, PersonToRelationshipRole>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, PersonToRelationshipRole>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.PersonToRelationshipRoles
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(p => keys.Contains(p.PersonToRelationshipRoleID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.PersonToRelationshipRoleID);
    }
}
