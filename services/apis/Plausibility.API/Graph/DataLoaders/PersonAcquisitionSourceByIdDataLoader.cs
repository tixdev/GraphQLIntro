using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.DataLoaders;

public class PersonAcquisitionSourceByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PlausibilityDbContext dbContext)
    : BatchDataLoader<int, PersonAcquisitionSource>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, PersonAcquisitionSource>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.PersonAcquisitionSources
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(p => keys.Contains(p.PersonAcquisitionSourceID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.PersonAcquisitionSourceID);
    }
}
