using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.DataLoaders;

public class BusinessDateTypeByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PlausibilityDbContext dbContext)
    : BatchDataLoader<int, BusinessDateType>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, BusinessDateType>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.BusinessDateTypes
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(p => keys.Contains(p.BusinessDateTypeID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.BusinessDateTypeID);
    }
}
