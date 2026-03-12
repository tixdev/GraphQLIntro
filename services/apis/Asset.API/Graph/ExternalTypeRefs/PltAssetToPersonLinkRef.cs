using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;

namespace Asset.API.Graph.ExternalTypeRefs;

[ObjectType("PltAssetToPersonLink")]
[Key("assetToPersonLinkID")]
[GraphQLName("PltAssetToPersonLink")]
public class PltAssetToPersonLinkRef
{
    [ReferenceResolver]
    public static async Task<PltAssetToPersonLinkRef> GetByIdAsync(int assetToPersonLinkID)
        => await Task.FromResult(new PltAssetToPersonLinkRef { AssetToPersonLinkID = assetToPersonLinkID });

    public int AssetToPersonLinkID { get; set; }
}
