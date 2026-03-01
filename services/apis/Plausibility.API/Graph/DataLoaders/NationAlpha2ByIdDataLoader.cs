using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.DataLoaders;

public class NationAlpha2ByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PlausibilityDbContext dbContext)
    : BatchDataLoader<int, NationAlpha2>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, NationAlpha2>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.NationAlpha2s
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(p => keys.Contains(p.NationAlpha2ID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.NationAlpha2ID);
    }
}
