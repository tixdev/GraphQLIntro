using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using Microsoft.EntityFrameworkCore;
using Person.API.Data;
using PersonModel = Person.API.Models.Person;

namespace Person.API.GraphQL;

public class Query
{
    [UseOffsetPaging(MaxPageSize = 200, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<PersonModel> GetPerson([Service] PersonContext context) => context.Person.AsNoTracking();
}
