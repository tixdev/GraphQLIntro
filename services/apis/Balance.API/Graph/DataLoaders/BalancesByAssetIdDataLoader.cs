using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenDonut;
using Microsoft.EntityFrameworkCore;
using Balance.API.Data;
using BalanceModel = Balance.API.Models.Balance;

namespace Balance.API.Graph.DataLoaders;

public class BalancesByAssetIdDataLoader : GroupedDataLoader<int, BalanceModel>
{
    private readonly BalanceContext _dbContext;

    public BalancesByAssetIdDataLoader(
        IBatchScheduler batchScheduler,
        DataLoaderOptions options,
        BalanceContext dbContext)
        : base(batchScheduler, options)
    {
        _dbContext = dbContext;
    }

    protected override async Task<ILookup<int, BalanceModel>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await _dbContext.Balances
            .Where(r => keys.Contains(r.AssetId))
            .ToListAsync(cancellationToken);

        return items.ToLookup(r => r.AssetId);
    }
}
