using Relationship.API.Models;

namespace Relationship.API.Graph.Types;

public class RelationshipToPersonType : ObjectType<RelationshipToPerson>
{
    protected override void Configure(IObjectTypeDescriptor<RelationshipToPerson> descriptor)
    {
        descriptor.Field(t => t.PltPersonToRelationshipRoleID).IsProjected();
    }
}
