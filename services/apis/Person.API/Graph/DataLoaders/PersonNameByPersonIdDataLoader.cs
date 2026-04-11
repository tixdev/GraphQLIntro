using Microsoft.EntityFrameworkCore;
using Person.API.Data;
using PersonNameModel = Person.API.Models.PersonName;

namespace Person.API.Graph.DataLoaders;

public class PersonNameByPersonIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PersonContext dbContext) : GroupedDataLoader<int, PersonNameModel>(batchScheduler, options)
{
    protected override async Task<ILookup<int, PersonNameModel>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.PersonName
            .AsNoTracking()
            .Where(n => keys.Contains(n.PersonID))
            .ToListAsync(cancellationToken);

        return items.ToLookup(n => n.PersonID);
    }
}
