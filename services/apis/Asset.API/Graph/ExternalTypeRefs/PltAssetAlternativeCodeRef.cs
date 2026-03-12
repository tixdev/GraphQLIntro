using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;

namespace Asset.API.Graph.ExternalTypeRefs;

[ObjectType("PltAssetAlternativeCode")]
[Key("assetAlternativeCodeID")]
[GraphQLName("PltAssetAlternativeCode")]
public class PltAssetAlternativeCodeRef
{
    [ReferenceResolver]
    public static async Task<PltAssetAlternativeCodeRef> GetByIdAsync(int assetAlternativeCodeID)
        => await Task.FromResult(new PltAssetAlternativeCodeRef { AssetAlternativeCodeID = assetAlternativeCodeID });

    public int AssetAlternativeCodeID { get; set; }
}
