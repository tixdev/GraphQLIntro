using Microsoft.EntityFrameworkCore;
using Relationship.API.Data;
using Relationship.API.Graph.ExternalTypeRefs;
using Relationship.API.Graph.TypeExtensions;

namespace Relationship.API.Graph.DataLoaders;

public class PersonsByRelationshipIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    RelationshipContext dbContext) : GroupedDataLoader<int, PersonRef>(batchScheduler, options)
{
    protected override async Task<ILookup<int, PersonRef>> LoadGroupedBatchAsync(
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
            .ToLookup(x => x.RelationshipID, x => new PersonRef { PersonID = x.PersonID });
    }
}
