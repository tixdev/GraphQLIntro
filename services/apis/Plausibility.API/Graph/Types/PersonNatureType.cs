using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class PersonNatureType : ObjectType<PersonNature>
{
    protected override void Configure(IObjectTypeDescriptor<PersonNature> descriptor)
    {
        var method = typeof(PersonNatureType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("personNatureID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<PersonNature?> GetByIdAsync(int personNatureID, PersonNatureByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(personNatureID);
    }
}
