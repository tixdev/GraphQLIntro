using Microsoft.EntityFrameworkCore;
using Person.API.Data;
using PersonDetailModel = Person.API.Models.PersonDetail;

namespace Person.API.Graph.DataLoaders;

public class PersonDetailByPersonIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PersonContext dbContext)
    : BatchDataLoader<int, PersonDetailModel?>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, PersonDetailModel?>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.PersonDetail
            .AsNoTracking()
            .Include(p => p.SensibleData)
            .Where(p => keys.Contains(p.PersonID))
            .ToListAsync(cancellationToken);

        return items
            .GroupBy(p => p.PersonID)
            .ToDictionary(g => g.Key, g => (PersonDetailModel?)g.First());
    }
}
