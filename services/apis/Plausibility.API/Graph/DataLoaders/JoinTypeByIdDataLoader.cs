using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.DataLoaders;

public class JoinTypeByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PlausibilityDbContext dbContext)
    : BatchDataLoader<int, JoinType>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, JoinType>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.JoinTypes
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(p => keys.Contains(p.JoinTypeID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.JoinTypeID);
    }
}
