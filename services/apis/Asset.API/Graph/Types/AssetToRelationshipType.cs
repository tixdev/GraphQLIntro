using Asset.API.Graph.ExternalTypeRefs;
using AssetModel = Asset.API.Models.AssetToRelationship;

namespace Asset.API.Graph.Types;

public class AssetToRelationshipType : ObjectType<AssetModel>
{
    protected override void Configure(IObjectTypeDescriptor<AssetModel> descriptor)
    {
        descriptor.Field("assetToRelationshipLink")
            .Resolve(ctx =>
            {
                var parent = ctx.Parent<AssetModel>();
                return new PltAssetToRelationshipLinkRef { AssetToRelationshipLinkID = parent.PltAssetToRelationshipLinkID };
            });
    }
}