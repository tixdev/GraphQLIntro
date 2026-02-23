using Microsoft.EntityFrameworkCore;
using Relationship.API.Data;
using RelationshipModel = Relationship.API.Models.Relationship;

namespace Relationship.API.GraphQL;

public class RelationshipByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    RelationshipContext dbContext)
    : BatchDataLoader<int, RelationshipModel>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, RelationshipModel>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.Relationships
            .Include(r => r.Name)
            .Where(r => keys.Contains(r.RelationshipID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(r => r.RelationshipID);
    }
}

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
            .Where(rtp => keys.Contains(rtp.PersonID) && rtp.ValidEndDate > now)
            .Include(rtp => rtp.Relationship)
            .ThenInclude(r => r.Name)
            .Select(rtp => new { rtp.PersonID, rtp.Relationship })
            .ToListAsync(cancellationToken);

        return items
            .DistinctBy(x => new { x.PersonID, x.Relationship.RelationshipID })
            .ToLookup(x => x.PersonID, x => x.Relationship);
    }
}