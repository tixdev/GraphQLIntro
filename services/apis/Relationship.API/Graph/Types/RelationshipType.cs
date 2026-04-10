using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Relationship.API.Graph.DataLoaders;
using RelationshipModel = Relationship.API.Models.Relationship;
using Relationship.API.Models;
using Relationship.API.Graph.ExternalTypeRefs;
using Relationship.API.Graph.TypeExtensions;
using Relationship.API.Graph.Resolvers;

namespace Relationship.API.Graph.Types;

public class RelationshipType : ObjectType<RelationshipModel>
{
    protected override void Configure(IObjectTypeDescriptor<RelationshipModel> descriptor)
    {
        var method = typeof(RelationshipType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("relationshipID").ResolveReferenceWith(method);

        descriptor.Field(t => t.Name).Type<RelationshipNameType>();

        descriptor.Field("persons")
            .ResolveWith<RelationshipResolvers>(r => r.GetPersons(default!, default!))
            .IsProjected(false);

        descriptor.Field(t => t.RelationshipID).IsProjected(true);
        descriptor.Field(t => t.PltRelationshipTypeID).IsProjected(true);
    }

    [ReferenceResolver]
    public static async Task<RelationshipModel?> GetByIdAsync(int relationshipID, RelationshipByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(relationshipID);
    }
}
