using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class PersonProfessionType : ObjectType<PersonProfession>
{
    protected override void Configure(IObjectTypeDescriptor<PersonProfession> descriptor)
    {
        var method = typeof(PersonProfessionType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("personProfessionID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<PersonProfession?> GetByIdAsync(int personProfessionID, PersonProfessionByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(personProfessionID);
    }
}
