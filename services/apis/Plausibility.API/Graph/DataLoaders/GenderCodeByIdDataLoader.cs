using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.DataLoaders;

public class GenderCodeByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PlausibilityDbContext dbContext)
    : BatchDataLoader<int, GenderCode>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, GenderCode>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.GenderCodes
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(p => keys.Contains(p.GenderCodeID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.GenderCodeID);
    }
}
