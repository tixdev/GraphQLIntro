using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.DataLoaders;

public class MaritalRegimeByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PlausibilityDbContext dbContext)
    : BatchDataLoader<int, MaritalRegime>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, MaritalRegime>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.MaritalRegimes
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(p => keys.Contains(p.MaritalRegimeID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.MaritalRegimeID);
    }
}
