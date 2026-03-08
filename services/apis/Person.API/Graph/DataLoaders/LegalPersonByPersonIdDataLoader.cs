using Microsoft.EntityFrameworkCore;
using Person.API.Data;
using LegalPersonModel = Person.API.Models.LegalPerson;

namespace Person.API.Graph.DataLoaders;

public class LegalPersonByPersonIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PersonContext dbContext)
    : BatchDataLoader<int, LegalPersonModel?>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, LegalPersonModel?>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.LegalPerson
            .AsNoTracking()
            .Include(l => l.SensibleData)
            .Where(l => keys.Contains(l.LegalPersonID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(l => l.LegalPersonID, l => (LegalPersonModel?)l);
    }
}
