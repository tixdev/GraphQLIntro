using Microsoft.EntityFrameworkCore;
using Person.API.Data;
using NaturalPersonModel = Person.API.Models.NaturalPerson;

namespace Person.API.Graph.DataLoaders;

public class NaturalPersonByPersonIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PersonContext dbContext)
    : BatchDataLoader<int, NaturalPersonModel?>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, NaturalPersonModel?>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.NaturalPerson
            .AsNoTracking()
            .Include(n => n.SensibleData)
            .Where(n => keys.Contains(n.NaturalPersonID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(n => n.NaturalPersonID, n => (NaturalPersonModel?)n);
    }
}
