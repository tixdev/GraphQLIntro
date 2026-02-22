using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using Microsoft.EntityFrameworkCore;
using PersonAPI.Data;
using PersonModel = PersonAPI.Models.Person;

namespace PersonAPI.GraphQL;

public class Query
{
    [UseOffsetPaging(MaxPageSize = 200, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<PersonModel> GetPerson([Service] PersonContext context) => context.People.AsNoTracking();
}
