using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class AssetToPersonLinkType : ObjectType<AssetToPersonLink>
{
    protected override void Configure(IObjectTypeDescriptor<AssetToPersonLink> descriptor)
    {
        descriptor.Name("PltAssetToPersonLink");
        var method = typeof(AssetToPersonLinkType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("assetToPersonLinkID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<AssetToPersonLink?> GetByIdAsync(int assetToPersonLinkID, AssetToPersonLinkByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(assetToPersonLinkID);
    }
}
