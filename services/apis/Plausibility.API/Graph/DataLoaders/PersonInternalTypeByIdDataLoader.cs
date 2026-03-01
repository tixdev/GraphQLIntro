using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.DataLoaders;

public class PersonInternalTypeByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PlausibilityDbContext dbContext)
    : BatchDataLoader<int, PersonInternalType>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, PersonInternalType>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.PersonInternalTypes
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(p => keys.Contains(p.PersonInternalTypeID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.PersonInternalTypeID);
    }
}
