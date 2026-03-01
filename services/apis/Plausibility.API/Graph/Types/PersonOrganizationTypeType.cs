using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class PersonOrganizationTypeType : ObjectType<PersonOrganizationType>
{
    protected override void Configure(IObjectTypeDescriptor<PersonOrganizationType> descriptor)
    {
        var method = typeof(PersonOrganizationTypeType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("personOrganizationTypeID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<PersonOrganizationType?> GetByIdAsync(int personOrganizationTypeID, PersonOrganizationTypeByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(personOrganizationTypeID);
    }
}
