using Microsoft.EntityFrameworkCore;
using Relationship.API.Data;
using RelationshipModel = Relationship.API.Models.Relationship;

namespace Relationship.API.Graph;

public class Query
{
    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 200, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<RelationshipModel> GetRelationship([Service] RelationshipContext context) => context.Relationships.AsNoTracking();
}
