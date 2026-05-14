using Microsoft.EntityFrameworkCore;
using Person.API.Data;
using PersonAlternativeCodeModel = Person.API.Models.PersonAlternativeCode;

namespace Person.API.Graph.DataLoaders;

public class PersonAlternativeCodeByPersonIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PersonContext dbContext) : GroupedDataLoader<int, PersonAlternativeCodeModel>(batchScheduler, options)
{
    protected override async Task<ILookup<int, PersonAlternativeCodeModel>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.PersonAlternativeCode
            .AsNoTracking()
            .Where(n => keys.Contains(n.PersonID))
            .ToListAsync(cancellationToken);

        return items.ToLookup(n => n.PersonID);
    }
}
