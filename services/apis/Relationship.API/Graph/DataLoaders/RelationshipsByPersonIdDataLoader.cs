using Microsoft.EntityFrameworkCore;
using Relationship.API.Data;
using RelationshipModel = Relationship.API.Models.Relationship;

namespace Relationship.API.Graph.DataLoaders;

public class RelationshipsByPersonIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    RelationshipContext dbContext) : GroupedDataLoader<int, RelationshipModel>(batchScheduler, options)
{
    protected override async Task<ILookup<int, RelationshipModel>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var items = await dbContext.RelationshipToPersons
            .AsNoTracking()
            .Where(rtp => keys.Contains(rtp.PersonID) && rtp.ValidEndDate > now)
            .Include(rtp => rtp.Relationship)
                .ThenInclude(r => r.Name)
            .ToListAsync(cancellationToken);

        return items
            .Where(rtp => rtp.Relationship != null)
            .Select(rtp => new { rtp.PersonID, rtp.Relationship })
            .DistinctBy(x => new { x.PersonID, x.Relationship.RelationshipID })
            .ToLookup(x => x.PersonID, x => x.Relationship);
    }
}
