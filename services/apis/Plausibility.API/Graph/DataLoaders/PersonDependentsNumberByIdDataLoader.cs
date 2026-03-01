using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.DataLoaders;

public class PersonDependentsNumberByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PlausibilityDbContext dbContext)
    : BatchDataLoader<int, PersonDependentsNumber>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, PersonDependentsNumber>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.PersonDependentsNumbers
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(p => keys.Contains(p.PersonDependentsNumberID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.PersonDependentsNumberID);
    }
}
