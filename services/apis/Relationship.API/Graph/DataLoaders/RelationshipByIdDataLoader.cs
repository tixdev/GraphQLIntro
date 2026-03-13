using Microsoft.EntityFrameworkCore;
using Relationship.API.Data;
using RelationshipModel = Relationship.API.Models.Relationship;

namespace Relationship.API.Graph.DataLoaders;

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
            .AsNoTracking()
            .Include(r => r.Name)
            .Where(r => keys.Contains(r.RelationshipID))
            .ToListAsync(cancellationToken);

        return items
            .DistinctBy(r => r.RelationshipID)
            .ToDictionary(r => r.RelationshipID);
    }
}
