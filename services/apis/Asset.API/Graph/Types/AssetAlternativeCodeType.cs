using Asset.API.Graph.ExternalTypeRefs;
using AssetModel = Asset.API.Models.AssetAlternativeCode;

namespace Asset.API.Graph.Types;

public class AssetAlternativeCodeType : ObjectType<AssetModel>
{
    protected override void Configure(IObjectTypeDescriptor<AssetModel> descriptor)
    {
        descriptor.Field("assetAlternativeCode")
            .Resolve(ctx =>
            {
                var parent = ctx.Parent<AssetModel>();
                return new PltAssetAlternativeCodeRef { AssetAlternativeCodeID = parent.PltAssetAlternativeCodeID };
            });
    }
}