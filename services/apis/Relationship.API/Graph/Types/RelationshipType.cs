using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Relationship.API.Graph.DataLoaders;
using RelationshipModel = Relationship.API.Models.Relationship;
using Relationship.API.Models;

namespace Relationship.API.Graph.Types;

public class RelationshipType : ObjectType<RelationshipModel>
{
    protected override void Configure(IObjectTypeDescriptor<RelationshipModel> descriptor)
    {
        var method = typeof(RelationshipType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("relationshipID").ResolveReferenceWith(method);

        descriptor.Field(t => t.Name).Type<RelationshipNameType>();
    }

    [ReferenceResolver]
    public static async Task<RelationshipModel?> GetByIdAsync(int id, RelationshipByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(id);
    }
}
