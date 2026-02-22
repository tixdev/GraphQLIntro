using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using Microsoft.EntityFrameworkCore;
using RelationshipAPI.Data;
using RelationshipModel = RelationshipAPI.Models.Relationship;

namespace RelationshipAPI.GraphQL;

public class Query
{
    [UseOffsetPaging(MaxPageSize = 200, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<RelationshipModel> GetRelationship([Service] RelationshipContext context) => context.Relationships.AsNoTracking();
}
