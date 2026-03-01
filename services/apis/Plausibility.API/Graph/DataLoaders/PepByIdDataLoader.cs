using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.DataLoaders;

public class PepByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PlausibilityDbContext dbContext)
    : BatchDataLoader<int, Pep>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, Pep>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.Peps
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(p => keys.Contains(p.PepID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.PepID);
    }
}
