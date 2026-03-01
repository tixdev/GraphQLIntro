using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.DataLoaders;

public class PersonCodingTypeByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PlausibilityDbContext dbContext)
    : BatchDataLoader<int, PersonCodingType>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, PersonCodingType>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.PersonCodingTypes
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(p => keys.Contains(p.PersonCodingTypeID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.PersonCodingTypeID);
    }
}
