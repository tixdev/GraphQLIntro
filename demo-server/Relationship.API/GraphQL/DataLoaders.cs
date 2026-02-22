using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenDonut;
using Microsoft.EntityFrameworkCore;
using Relationship.API.Data;
using RelationshipModel = Relationship.API.Models.Relationship;

namespace Relationship.API.GraphQL;

public class RelationshipByIdDataLoader : BatchDataLoader<int, RelationshipModel>
{
    private readonly RelationshipContext _dbContext;

    public RelationshipByIdDataLoader(
        IBatchScheduler batchScheduler,
        DataLoaderOptions options,
        RelationshipContext dbContext)
        : base(batchScheduler, options)
    {
        _dbContext = dbContext;
    }

    protected override async Task<IReadOnlyDictionary<int, RelationshipModel>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await _dbContext.Relationships
            .Where(r => keys.Contains(r.RelationshipID))
            .ToListAsync(cancellationToken);
            
        return items.ToDictionary(r => r.RelationshipID);
    }
}

public class RelationshipsByPersonIdDataLoader : GroupedDataLoader<int, RelationshipModel>
{
    private readonly RelationshipContext _dbContext;

    public RelationshipsByPersonIdDataLoader(
        IBatchScheduler batchScheduler,
        DataLoaderOptions options,
        RelationshipContext dbContext)
        : base(batchScheduler, options)
    {
        _dbContext = dbContext;
    }

    protected override async Task<ILookup<int, RelationshipModel>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await _dbContext.Relationships
            .Where(r => keys.Contains(r.PersonId))
            .ToListAsync(cancellationToken);

        return items.ToLookup(r => r.PersonId);
    }
}
