using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;

namespace Asset.API.Graph.ExternalTypeRefs;

[ObjectType("PltAssetToRelationshipLink")]
[Key("assetToRelationshipLinkID")]
[GraphQLName("PltAssetToRelationshipLink")]
public class PltAssetToRelationshipLinkRef
{
    [ReferenceResolver]
    public static async Task<PltAssetToRelationshipLinkRef> GetByIdAsync(int assetToRelationshipLinkID)
        => await Task.FromResult(new PltAssetToRelationshipLinkRef { AssetToRelationshipLinkID = assetToRelationshipLinkID });

    public int AssetToRelationshipLinkID { get; set; }
}
