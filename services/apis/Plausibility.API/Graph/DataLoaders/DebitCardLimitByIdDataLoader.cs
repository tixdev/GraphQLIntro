using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.DataLoaders;

public class DebitCardLimitByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PlausibilityDbContext dbContext)
    : BatchDataLoader<int, DebitCardLimit>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, DebitCardLimit>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.DebitCardLimits
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(p => keys.Contains(p.DebitCardLimitID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.DebitCardLimitID);
    }
}
