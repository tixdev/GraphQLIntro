using Microsoft.EntityFrameworkCore;
using Person.API.Data;
using PersonModel = Person.API.Models.Person;

namespace Person.API.GraphQL;

public class Query
{
    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<PersonModel> GetPerson([Service] PersonContext context) =>
        context.Person
            .Include(p => p.PersonDetail).ThenInclude(d => d!.SensibleData)
            .Include(p => p.NaturalPerson).ThenInclude(n => n!.SensibleData)
            .Include(p => p.LegalPerson).ThenInclude(l => l!.SensibleData)
            .Include(p => p.InternalPerson)
            .Include(p => p.GroupPerson).ThenInclude(g => g!.SensibleData)
            .Include(p => p.PersonOnlineService)
            .AsNoTracking();
}
