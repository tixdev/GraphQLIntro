using Asset.API.Graph.ExternalTypeRefs;
using AssetModel = Asset.API.Models.AssetDebitCardLimit;

namespace Asset.API.Graph.Types;

public class AssetDebitCardLimitType : ObjectType<AssetModel>
{
    protected override void Configure(IObjectTypeDescriptor<AssetModel> descriptor)
    {
        descriptor.Field("debitCardLimit")
            .Resolve(ctx =>
            {
                var parent = ctx.Parent<AssetModel>();
                return new PltDebitCardLimitRef { DebitCardLimitID = parent.PltDebitCardLimitID };
            });
    }
}