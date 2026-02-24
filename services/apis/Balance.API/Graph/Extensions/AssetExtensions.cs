using HotChocolate;
using HotChocolate.Types;
using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Balance.API.Graph.DataLoaders;
using BalanceModel = Balance.API.Models.Balance;

namespace Balance.API.Graph.Extensions;

[ObjectType("Asset")]
[HotChocolate.ApolloFederation.Types.Key("assetID")]
public class AssetRef
{
    [ReferenceResolver]
    public static async Task<AssetRef> GetByIdAsync(int assetID)
        => await Task.FromResult(new AssetRef { AssetID = assetID });

    public int AssetID { get; set; }

    public async Task<BalanceModel[]> GetBalances(
        [Parent] AssetRef asset,
        BalancesByAssetIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(asset.AssetID);
    }
}
