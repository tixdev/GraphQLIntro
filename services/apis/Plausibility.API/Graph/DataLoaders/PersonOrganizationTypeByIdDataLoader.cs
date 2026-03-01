using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.DataLoaders;

public class PersonOrganizationTypeByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PlausibilityDbContext dbContext)
    : BatchDataLoader<int, PersonOrganizationType>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, PersonOrganizationType>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.PersonOrganizationTypes
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(p => keys.Contains(p.PersonOrganizationTypeID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.PersonOrganizationTypeID);
    }
}
