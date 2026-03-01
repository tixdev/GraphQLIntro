using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class PersonInternalTypeType : ObjectType<PersonInternalType>
{
    protected override void Configure(IObjectTypeDescriptor<PersonInternalType> descriptor)
    {
        var method = typeof(PersonInternalTypeType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("personInternalTypeID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<PersonInternalType?> GetByIdAsync(int personInternalTypeID, PersonInternalTypeByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(personInternalTypeID);
    }
}
