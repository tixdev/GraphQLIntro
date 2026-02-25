using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Relationship.API.Graph.DataLoaders;
using RelationshipModel = Relationship.API.Models.Relationship;
using Relationship.API.Models;
using Relationship.API.Graph.Extensions;

namespace Relationship.API.Graph.Types;

public class RelationshipType : ObjectType<RelationshipModel>
{
    protected override void Configure(IObjectTypeDescriptor<RelationshipModel> descriptor)
    {
        var method = typeof(RelationshipType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("relationshipID").ResolveReferenceWith(method);

        descriptor.Field(t => t.Name).Type<RelationshipNameType>();

        descriptor.Field("persons")
            .ResolveWith<RelationshipType>(t => t.GetPersons(default!, default!));
    }

    [ReferenceResolver]
    public static async Task<RelationshipModel?> GetByIdAsync(int relationshipID, RelationshipByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(relationshipID);
    }

    public async Task<IEnumerable<PersonExtensions>> GetPersons(
        [Parent] RelationshipModel relationship,
        PersonsByRelationshipIdDataLoader dataLoader)
    {
        var results = await dataLoader.LoadAsync(relationship.RelationshipID);
        return results ?? Array.Empty<PersonExtensions>();
    }
}
