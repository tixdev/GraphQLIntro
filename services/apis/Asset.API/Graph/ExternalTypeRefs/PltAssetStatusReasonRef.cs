using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;

namespace Asset.API.Graph.ExternalTypeRefs;

[ObjectType("PltAssetStatusReason")]
[Key("assetStatusReasonID")]
[GraphQLName("PltAssetStatusReason")]
public class PltAssetStatusReasonRef
{
    [ReferenceResolver]
    public static async Task<PltAssetStatusReasonRef> GetByIdAsync(int assetStatusReasonID)
        => await Task.FromResult(new PltAssetStatusReasonRef { AssetStatusReasonID = assetStatusReasonID });

    public int AssetStatusReasonID { get; set; }
}
