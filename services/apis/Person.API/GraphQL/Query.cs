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
    public IQueryable<PersonModel> GetPerson([Service] PersonContext context) => context.Person.AsNoTracking();
}
