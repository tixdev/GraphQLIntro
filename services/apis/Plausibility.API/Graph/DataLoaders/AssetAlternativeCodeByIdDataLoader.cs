using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.DataLoaders;

public class AssetAlternativeCodeByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PlausibilityDbContext dbContext)
    : BatchDataLoader<int, AssetAlternativeCode>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, AssetAlternativeCode>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.AssetAlternativeCodes
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(p => keys.Contains(p.AssetAlternativeCodeID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.AssetAlternativeCodeID);
    }
}
