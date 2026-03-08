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
                .ThenInclude(n => n!.SensibleData)
            .Include(p => p.LegalPerson)
                .ThenInclude(l => l!.SensibleData)
            .Include(p => p.GroupPerson)
                .ThenInclude(g => g!.SensibleData)
            .Include(p => p.InternalPerson)
            .Where(p => keys.Contains(p.PersonID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.PersonID);
    }
}
