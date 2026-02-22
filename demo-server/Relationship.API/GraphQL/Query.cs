using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using Microsoft.EntityFrameworkCore;
using Relationship.API.Data;
using RelationshipModel = Relationship.API.Models.Relationship;

namespace Relationship.API.GraphQL;

public class Query
{
    [UseOffsetPaging(MaxPageSize = 200, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<RelationshipModel> GetRelationship([Service] RelationshipContext context) => context.Relationships.AsNoTracking();
}
