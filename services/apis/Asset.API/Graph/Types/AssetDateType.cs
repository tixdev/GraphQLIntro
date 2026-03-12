using Asset.API.Graph.ExternalTypeRefs;
using AssetModel = Asset.API.Models.AssetDate;

namespace Asset.API.Graph.Types;

public class AssetDateType : ObjectType<AssetModel>
{
    protected override void Configure(IObjectTypeDescriptor<AssetModel> descriptor)
    {
        descriptor.Field("businessDateType")
            .Resolve(ctx =>
            {
                var parent = ctx.Parent<AssetModel>();
                return new PltBusinessDateTypeRef { BusinessDateTypeID = parent.PltBusinessDateTypeID };
            });
    }
}