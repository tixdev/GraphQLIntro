using Asset.API.Graph.ExternalTypeRefs;
using AssetModel = Asset.API.Models.AssetLifeCycleStatus;

namespace Asset.API.Graph.Types;

public class AssetLifeCycleStatusType : ObjectType<AssetModel>
{
    protected override void Configure(IObjectTypeDescriptor<AssetModel> descriptor)
    {
        descriptor.Field("assetStatus")
            .Resolve(ctx =>
            {
                var parent = ctx.Parent<AssetModel>();
                return new PltAssetStatusRef { AssetStatusID = parent.PltAssetStatusID };
            });
        
        descriptor.Field("assetStatusReason")
            .Resolve(ctx =>
            {
                var parent = ctx.Parent<AssetModel>();
                return parent.PltAssetStatusReasonID.HasValue ? new PltAssetStatusReasonRef { AssetStatusReasonID = parent.PltAssetStatusReasonID.Value } : null;
            });
    }
}