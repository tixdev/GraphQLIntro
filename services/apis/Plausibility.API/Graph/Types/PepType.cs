using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class PepType : ObjectType<Pep>
{
    protected override void Configure(IObjectTypeDescriptor<Pep> descriptor)
    {
        var method = typeof(PepType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("pepID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<Pep?> GetByIdAsync(int pepID, PepByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(pepID);
    }
}
