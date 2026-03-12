using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using AssetModel = Asset.API.Models.Asset;
using Asset.API.Graph.DataLoaders;
using Asset.API.Graph.ExternalTypeRefs;
using Asset.API.Graph.Resolvers;

namespace Asset.API.Graph.Types;

public class AssetType : ObjectType<AssetModel>
{
    protected override void Configure(IObjectTypeDescriptor<AssetModel> descriptor)
    {
        var method = typeof(AssetType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("assetID").ResolveReferenceWith(method);

        descriptor.Field(t => t.AssetID).IsProjected();
        descriptor.Field(t => t.GroupBankID).IsProjected();
        descriptor.Field(t => t.RelationshipID).IsProjected(true);
        descriptor.Field(t => t.PltAssetTypeID).IsProjected(true);

        descriptor.Field(t => t.AssetAlternativeCodes).IsProjected(false);
        descriptor.Field(t => t.AssetDates).IsProjected(false);
        descriptor.Field(t => t.AssetDebitCards).IsProjected(false);
        descriptor.Field(t => t.AssetDetails).IsProjected(false);
        descriptor.Field(t => t.AssetIntermediaries).IsProjected(false);
        descriptor.Field(t => t.AssetLifeCycleStatuses).IsProjected(false);
        descriptor.Field(t => t.AssetNotes).IsProjected(false);
        descriptor.Field(t => t.AssetOthers).IsProjected(false);
        descriptor.Field(t => t.AssetSafetyBoxes).IsProjected(false);
        descriptor.Field(t => t.AssetToConditions).IsProjected(false);
        descriptor.Field(t => t.AssetToPeople).IsProjected(false);
        descriptor.Field(t => t.AssetToRelationships).IsProjected(false);
        descriptor.Field(t => t.AssetVisaDebitCards).IsProjected(false);

        descriptor.Field("assetDates")
            .ResolveWith<AssetResolvers>(r => r.GetAssetDates(default!, default!));

        descriptor.Field("assetDebitCards")
            .ResolveWith<AssetResolvers>(r => r.GetAssetDebitCards(default!, default!));

        descriptor.Field("assetDetails")
            .ResolveWith<AssetResolvers>(r => r.GetAssetDetails(default!, default!));

        descriptor.Field("assetIntermediaries")
            .ResolveWith<AssetResolvers>(r => r.GetAssetIntermediaries(default!, default!));

        descriptor.Field("assetLifeCycleStatuses")
            .ResolveWith<AssetResolvers>(r => r.GetAssetLifeCycleStatuses(default!, default!));

        descriptor.Field("assetNotes")
            .ResolveWith<AssetResolvers>(r => r.GetAssetNotes(default!, default!));

        descriptor.Field("assetOthers")
            .ResolveWith<AssetResolvers>(r => r.GetAssetOthers(default!, default!));

        descriptor.Field("assetSafetyBoxes")
            .ResolveWith<AssetResolvers>(r => r.GetAssetSafetyBoxes(default!, default!));

        descriptor.Field("assetToConditions")
            .ResolveWith<AssetResolvers>(r => r.GetAssetToConditions(default!, default!));

        descriptor.Field("assetToPeople")
            .ResolveWith<AssetResolvers>(r => r.GetAssetToPeople(default!, default!));

        descriptor.Field("assetToRelationships")
            .ResolveWith<AssetResolvers>(r => r.GetAssetToRelationships(default!, default!));

        descriptor.Field("assetVisaDebitCards")
            .ResolveWith<AssetResolvers>(r => r.GetAssetVisaDebitCards(default!, default!));

        descriptor.Field("assetAlternativeCodes")
            .ResolveWith<AssetResolvers>(r => r.GetAssetAlternativeCodes(default!, default!));

        descriptor.Field("relationship")
            .Resolve(ctx =>
            {
                var parent = ctx.Parent<AssetModel>();
                return new RelationshipRef { RelationshipID = parent.RelationshipID };
            });

        descriptor.Field("assetType")
            .Resolve(ctx =>
            {
                var parent = ctx.Parent<AssetModel>();
                return new PltAssetTypeRef { AssetTypeID = parent.PltAssetTypeID };
            });
    }

    [ReferenceResolver]
    public static async Task<AssetModel?> GetByIdAsync(
        int id,
        AssetByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(id);
    }
}
