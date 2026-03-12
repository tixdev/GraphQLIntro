using Asset.API.Graph.ExternalTypeRefs;
using AssetModel = Asset.API.Models.AssetToPerson;

namespace Asset.API.Graph.Types;

public class AssetToPersonType : ObjectType<AssetModel>
{
    protected override void Configure(IObjectTypeDescriptor<AssetModel> descriptor)
    {
        descriptor.Field("assetToPersonLink")
            .Resolve(ctx =>
            {
                var parent = ctx.Parent<AssetModel>();
                return new PltAssetToPersonLinkRef { AssetToPersonLinkID = parent.PltAssetToPersonLinkID };
            });
        
        descriptor.Field("powerOfSignature")
            .Resolve(ctx =>
            {
                var parent = ctx.Parent<AssetModel>();
                return parent.PltPowerOfSignatureID.HasValue ? new PltPowerOfSignatureRef { PowerOfSignatureID = parent.PltPowerOfSignatureID.Value } : null;
            });
    }
}