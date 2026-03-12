using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class AssetToRelationshipLinkType : ObjectType<AssetToRelationshipLink>
{
    protected override void Configure(IObjectTypeDescriptor<AssetToRelationshipLink> descriptor)
    {
        descriptor.Name("PltAssetToRelationshipLink");
        var method = typeof(AssetToRelationshipLinkType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("assetToRelationshipLinkID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<AssetToRelationshipLink?> GetByIdAsync(int assetToRelationshipLinkID, AssetToRelationshipLinkByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(assetToRelationshipLinkID);
    }
}
