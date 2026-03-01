using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.DataLoaders;

public class NogaByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PlausibilityDbContext dbContext)
    : BatchDataLoader<int, Noga>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, Noga>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.Nogas
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(p => keys.Contains(p.NogaID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.NogaID);
    }
}
