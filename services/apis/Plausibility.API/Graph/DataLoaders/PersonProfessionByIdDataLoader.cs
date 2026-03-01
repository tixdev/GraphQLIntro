using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.DataLoaders;

public class PersonProfessionByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PlausibilityDbContext dbContext)
    : BatchDataLoader<int, PersonProfession>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, PersonProfession>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.PersonProfessions
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(p => keys.Contains(p.PersonProfessionID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.PersonProfessionID);
    }
}
