using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;

namespace Asset.API.Graph.ExternalTypeRefs;

[ObjectType("PltAssetStatus")]
[Key("assetStatusID")]
[GraphQLName("PltAssetStatus")]
public class PltAssetStatusRef
{
    [ReferenceResolver]
    public static async Task<PltAssetStatusRef> GetByIdAsync(int assetStatusID)
        => await Task.FromResult(new PltAssetStatusRef { AssetStatusID = assetStatusID });

    public int AssetStatusID { get; set; }
}
