using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class NogaType : ObjectType<Noga>
{
    protected override void Configure(IObjectTypeDescriptor<Noga> descriptor)
    {
        var method = typeof(NogaType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("nogaID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<Noga?> GetByIdAsync(int nogaID, NogaByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(nogaID);
    }
}
