using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;

namespace Asset.API.Graph.ExternalTypeRefs;

[ObjectType("PltAssetType")]
[Key("assetTypeID")]
[GraphQLName("PltAssetType")]
public class PltAssetTypeRef
{
    [ReferenceResolver]
    public static async Task<PltAssetTypeRef> GetByIdAsync(int assetTypeID)
        => await Task.FromResult(new PltAssetTypeRef { AssetTypeID = assetTypeID });

    public int AssetTypeID { get; set; }
}
