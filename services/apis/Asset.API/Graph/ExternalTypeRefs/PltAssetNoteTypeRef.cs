using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;

namespace Asset.API.Graph.ExternalTypeRefs;

[ObjectType("PltAssetNoteType")]
[Key("assetNoteTypeID")]
[GraphQLName("PltAssetNoteType")]
public class PltAssetNoteTypeRef
{
    [ReferenceResolver]
    public static async Task<PltAssetNoteTypeRef> GetByIdAsync(int assetNoteTypeID)
        => await Task.FromResult(new PltAssetNoteTypeRef { AssetNoteTypeID = assetNoteTypeID });

    public int AssetNoteTypeID { get; set; }
}
