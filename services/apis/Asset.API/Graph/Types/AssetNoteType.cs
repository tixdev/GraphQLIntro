using Asset.API.Graph.ExternalTypeRefs;
using AssetModel = Asset.API.Models.AssetNote;

namespace Asset.API.Graph.Types;

public class AssetNoteType : ObjectType<AssetModel>
{
    protected override void Configure(IObjectTypeDescriptor<AssetModel> descriptor)
    {
        descriptor.Field("assetNoteType")
            .Resolve(ctx =>
            {
                var parent = ctx.Parent<AssetModel>();
                return new PltAssetNoteTypeRef { AssetNoteTypeID = parent.PltAssetNoteTypeID };
            });
    }
}