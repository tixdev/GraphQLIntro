using Microsoft.EntityFrameworkCore;
using Person.API.Data;
using GroupPersonModel = Person.API.Models.GroupPerson;

namespace Person.API.Graph.DataLoaders;

public class GroupPersonByPersonIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PersonContext dbContext)
    : BatchDataLoader<int, GroupPersonModel?>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, GroupPersonModel?>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.GroupPerson
            .AsNoTracking()
            .Include(g => g.SensibleData)
            .Where(g => keys.Contains(g.GroupPersonID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(g => g.GroupPersonID, g => (GroupPersonModel?)g);
    }
}
