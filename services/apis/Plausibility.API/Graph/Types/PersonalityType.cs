using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class PersonalityType : ObjectType<Personality>
{
    protected override void Configure(IObjectTypeDescriptor<Personality> descriptor)
    {
        var method = typeof(PersonalityType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("personalityID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<Personality?> GetByIdAsync(int personalityID, PersonalityByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(personalityID);
    }
}
