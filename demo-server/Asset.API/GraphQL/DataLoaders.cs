using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenDonut;
using Microsoft.EntityFrameworkCore;
using Asset.API.Data;
using AssetModel = Asset.API.Models.Asset;

namespace Asset.API.GraphQL;

public class AssetByIdDataLoader : BatchDataLoader<int, AssetModel>
{
    private readonly AssetContext _dbContext;

    public AssetByIdDataLoader(
        IBatchScheduler batchScheduler,
        DataLoaderOptions options,
        AssetContext dbContext)
        : base(batchScheduler, options)
    {
        _dbContext = dbContext;
    }

    protected override async Task<IReadOnlyDictionary<int, AssetModel>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await _dbContext.Assets
            .Where(r => keys.Contains(r.AssetID))
            .ToListAsync(cancellationToken);
            
        return items.ToDictionary(r => r.AssetID);
    }
}

public class AssetsByRelationshipIdDataLoader : GroupedDataLoader<int, AssetModel>
{
    private readonly AssetContext _dbContext;

    public AssetsByRelationshipIdDataLoader(
        IBatchScheduler batchScheduler,
        DataLoaderOptions options,
        AssetContext dbContext)
        : base(batchScheduler, options)
    {
        _dbContext = dbContext;
    }

    protected override async Task<ILookup<int, AssetModel>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await _dbContext.Assets
            .Where(r => keys.Contains(r.RelationshipId))
            .ToListAsync(cancellationToken);

        return items.ToLookup(r => r.RelationshipId);
    }
}
