using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;

namespace Asset.API.Graph.ExternalTypeRefs;

[ObjectType("PltAssetToAssetLink")]
[Key("assetToAssetLinkID")]
[GraphQLName("PltAssetToAssetLink")]
public class PltAssetToAssetLinkRef
{
    [ReferenceResolver]
    public static async Task<PltAssetToAssetLinkRef> GetByIdAsync(int assetToAssetLinkID)
        => await Task.FromResult(new PltAssetToAssetLinkRef { AssetToAssetLinkID = assetToAssetLinkID });

    public int AssetToAssetLinkID { get; set; }
}
