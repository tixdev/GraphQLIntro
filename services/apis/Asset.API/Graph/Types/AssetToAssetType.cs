using Asset.API.Graph.ExternalTypeRefs;
using AssetModel = Asset.API.Models.AssetToAsset;

namespace Asset.API.Graph.Types;

public class AssetToAssetType : ObjectType<AssetModel>
{
    protected override void Configure(IObjectTypeDescriptor<AssetModel> descriptor)
    {
        descriptor.Field("assetToAssetLink")
            .Resolve(ctx =>
            {
                var parent = ctx.Parent<AssetModel>();
                return new PltAssetToAssetLinkRef { AssetToAssetLinkID = parent.PltAssetToAssetLinkID };
            });
    }
}