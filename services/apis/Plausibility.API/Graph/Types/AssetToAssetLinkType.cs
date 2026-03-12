using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class AssetToAssetLinkType : ObjectType<AssetToAssetLink>
{
    protected override void Configure(IObjectTypeDescriptor<AssetToAssetLink> descriptor)
    {
        descriptor.Name("PltAssetToAssetLink");
        var method = typeof(AssetToAssetLinkType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("assetToAssetLinkID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<AssetToAssetLink?> GetByIdAsync(int assetToAssetLinkID, AssetToAssetLinkByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(assetToAssetLinkID);
    }
}
