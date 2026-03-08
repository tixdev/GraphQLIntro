using Microsoft.EntityFrameworkCore;
using Person.API.Data;
using InternalPersonModel = Person.API.Models.InternalPerson;

namespace Person.API.Graph.DataLoaders;

public class InternalPersonByPersonIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PersonContext dbContext)
    : BatchDataLoader<int, InternalPersonModel?>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, InternalPersonModel?>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.InternalPerson
            .AsNoTracking()
            .Where(i => keys.Contains(i.InternalPersonID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(i => i.InternalPersonID, i => (InternalPersonModel?)i);
    }
}
