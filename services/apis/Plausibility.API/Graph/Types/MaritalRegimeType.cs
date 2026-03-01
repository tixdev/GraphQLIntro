using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class MaritalRegimeType : ObjectType<MaritalRegime>
{
    protected override void Configure(IObjectTypeDescriptor<MaritalRegime> descriptor)
    {
        var method = typeof(MaritalRegimeType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("maritalRegimeID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<MaritalRegime?> GetByIdAsync(int maritalRegimeID, MaritalRegimeByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(maritalRegimeID);
    }
}
