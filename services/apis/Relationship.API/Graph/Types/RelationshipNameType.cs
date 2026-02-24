using HotChocolate.Types;
using Relationship.API.Models;

namespace Relationship.API.Graph.Types;

public class RelationshipNameType : ObjectType<RelationshipName>
{
    protected override void Configure(IObjectTypeDescriptor<RelationshipName> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(t => t.Name);
    }
}
