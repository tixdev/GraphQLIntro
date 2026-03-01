using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.DataLoaders;

public class PersonMaritalStatusByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PlausibilityDbContext dbContext)
    : BatchDataLoader<int, PersonMaritalStatus>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, PersonMaritalStatus>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.PersonMaritalStatuss
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(p => keys.Contains(p.PersonMaritalStatusID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.PersonMaritalStatusID);
    }
}
