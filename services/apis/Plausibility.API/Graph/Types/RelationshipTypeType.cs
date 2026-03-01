using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class RelationshipTypeType : ObjectType<RelationshipType>
{
    protected override void Configure(IObjectTypeDescriptor<RelationshipType> descriptor)
    {
        var method = typeof(RelationshipTypeType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("relationshipTypeID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<RelationshipType?> GetByIdAsync(int relationshipTypeID, RelationshipTypeByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(relationshipTypeID);
    }
}
