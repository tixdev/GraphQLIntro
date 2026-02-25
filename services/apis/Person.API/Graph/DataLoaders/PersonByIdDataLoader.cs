using Microsoft.EntityFrameworkCore;
using Person.API.Data;
using PersonModel = Person.API.Models.Person;

namespace Person.API.Graph.DataLoaders;

public class PersonByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PersonContext dbContext)
    : BatchDataLoader<int, PersonModel>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, PersonModel>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.Person
            .AsNoTracking()
            .Include(p => p.NaturalPerson)
            .Include(p => p.LegalPerson)
            .Include(p => p.GroupPerson)
            .Include(p => p.InternalPerson)
            .Where(p => keys.Contains(p.PersonID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.PersonID);
    }
}
