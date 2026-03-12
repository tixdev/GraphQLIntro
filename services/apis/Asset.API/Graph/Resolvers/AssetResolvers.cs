using Asset.API.Graph.DataLoaders;
using AssetModel = Asset.API.Models.Asset;

namespace Asset.API.Graph.Resolvers;

public class AssetResolvers
{
    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<Asset.API.Models.AssetDate>> GetAssetDates(
        [Parent] AssetModel asset,
        AssetDatesByAssetIdDataLoader dataLoader)
    {
        var results = await dataLoader.LoadAsync(asset.AssetID);
        return results ?? Array.Empty<Asset.API.Models.AssetDate>();
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<Asset.API.Models.AssetDebitCard>> GetAssetDebitCards(
        [Parent] AssetModel asset,
        AssetDebitCardsByAssetIdDataLoader dataLoader)
    {
        var results = await dataLoader.LoadAsync(asset.AssetID);
        return results ?? Array.Empty<Asset.API.Models.AssetDebitCard>();
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<Asset.API.Models.AssetDetail>> GetAssetDetails(
        [Parent] AssetModel asset,
        AssetDetailsByAssetIdDataLoader dataLoader)
    {
        var results = await dataLoader.LoadAsync(asset.AssetID);
        return results ?? Array.Empty<Asset.API.Models.AssetDetail>();
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<Asset.API.Models.AssetIntermediary>> GetAssetIntermediaries(
        [Parent] AssetModel asset,
        AssetIntermediariesByAssetIdDataLoader dataLoader)
    {
        var results = await dataLoader.LoadAsync(asset.AssetID);
        return results ?? Array.Empty<Asset.API.Models.AssetIntermediary>();
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<Asset.API.Models.AssetLifeCycleStatus>> GetAssetLifeCycleStatuses(
        [Parent] AssetModel asset,
        AssetLifeCycleStatusesByAssetIdDataLoader dataLoader)
    {
        var results = await dataLoader.LoadAsync(asset.AssetID);
        return results ?? Array.Empty<Asset.API.Models.AssetLifeCycleStatus>();
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<Asset.API.Models.AssetNote>> GetAssetNotes(
        [Parent] AssetModel asset,
        AssetNotesByAssetIdDataLoader dataLoader)
    {
        var results = await dataLoader.LoadAsync(asset.AssetID);
        return results ?? Array.Empty<Asset.API.Models.AssetNote>();
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<Asset.API.Models.AssetOther>> GetAssetOthers(
        [Parent] AssetModel asset,
        AssetOthersByAssetIdDataLoader dataLoader)
    {
        var results = await dataLoader.LoadAsync(asset.AssetID);
        return results ?? Array.Empty<Asset.API.Models.AssetOther>();
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<Asset.API.Models.AssetSafetyBox>> GetAssetSafetyBoxes(
        [Parent] AssetModel asset,
        AssetSafetyBoxesByAssetIdDataLoader dataLoader)
    {
        var results = await dataLoader.LoadAsync(asset.AssetID);
        return results ?? Array.Empty<Asset.API.Models.AssetSafetyBox>();
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<Asset.API.Models.AssetToCondition>> GetAssetToConditions(
        [Parent] AssetModel asset,
        AssetToConditionsByAssetIdDataLoader dataLoader)
    {
        var results = await dataLoader.LoadAsync(asset.AssetID);
        return results ?? Array.Empty<Asset.API.Models.AssetToCondition>();
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<Asset.API.Models.AssetToPerson>> GetAssetToPeople(
        [Parent] AssetModel asset,
        AssetToPeopleByAssetIdDataLoader dataLoader)
    {
        var results = await dataLoader.LoadAsync(asset.AssetID);
        return results ?? Array.Empty<Asset.API.Models.AssetToPerson>();
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<Asset.API.Models.AssetToRelationship>> GetAssetToRelationships(
        [Parent] AssetModel asset,
        AssetToRelationshipsByAssetIdDataLoader dataLoader)
    {
        var results = await dataLoader.LoadAsync(asset.AssetID);
        return results ?? Array.Empty<Asset.API.Models.AssetToRelationship>();
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<Asset.API.Models.AssetVisaDebitCard>> GetAssetVisaDebitCards(
        [Parent] AssetModel asset,
        AssetVisaDebitCardsByAssetIdDataLoader dataLoader)
    {
        var results = await dataLoader.LoadAsync(asset.AssetID);
        return results ?? Array.Empty<Asset.API.Models.AssetVisaDebitCard>();
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<Asset.API.Models.AssetAlternativeCode>> GetAssetAlternativeCodes(
        [Parent] AssetModel asset,
        AssetAlternativeCodesByAssetIdDataLoader dataLoader)
    {
        var results = await dataLoader.LoadAsync(asset.AssetID);
        return results ?? Array.Empty<Asset.API.Models.AssetAlternativeCode>();
    }
}
