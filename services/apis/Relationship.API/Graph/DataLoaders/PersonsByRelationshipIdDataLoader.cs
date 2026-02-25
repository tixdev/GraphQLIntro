using Microsoft.EntityFrameworkCore;
using Relationship.API.Data;
using Relationship.API.Graph.Extensions;

namespace Relationship.API.Graph.DataLoaders;

public class PersonsByRelationshipIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    RelationshipContext dbContext) : GroupedDataLoader<int, PersonExtensions>(batchScheduler, options)
{
    protected override async Task<ILookup<int, PersonExtensions>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var items = await dbContext.RelationshipToPersons
            .AsNoTracking()
            .Where(rtp => keys.Contains(rtp.RelationshipID) && rtp.ValidEndDate > now)
            .Select(rtp => new { rtp.RelationshipID, rtp.PersonID })
            .ToListAsync(cancellationToken);

        return items
            .DistinctBy(x => new { x.RelationshipID, x.PersonID })
            .ToLookup(x => x.RelationshipID, x => new PersonExtensions { PersonID = x.PersonID });
    }
}
