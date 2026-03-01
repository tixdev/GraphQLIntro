using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class NationAlpha2Type : ObjectType<NationAlpha2>
{
    protected override void Configure(IObjectTypeDescriptor<NationAlpha2> descriptor)
    {
        var method = typeof(NationAlpha2Type).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("nationAlpha2ID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<NationAlpha2?> GetByIdAsync(int nationAlpha2ID, NationAlpha2ByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(nationAlpha2ID);
    }
}
