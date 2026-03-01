using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.DataLoaders;

public class PersonNatureByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PlausibilityDbContext dbContext)
    : BatchDataLoader<int, PersonNature>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, PersonNature>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.PersonNatures
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(p => keys.Contains(p.PersonNatureID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.PersonNatureID);
    }
}
