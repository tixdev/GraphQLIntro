using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class PersonToRelationshipRoleType : ObjectType<PersonToRelationshipRole>
{
    protected override void Configure(IObjectTypeDescriptor<PersonToRelationshipRole> descriptor)
    {
        var method = typeof(PersonToRelationshipRoleType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("personToRelationshipRoleID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<PersonToRelationshipRole?> GetByIdAsync(int personToRelationshipRoleID, PersonToRelationshipRoleByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(personToRelationshipRoleID);
    }
}
