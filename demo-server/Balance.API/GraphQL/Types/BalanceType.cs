using HotChocolate.Types;
using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using HotChocolate.Data;
using System.Reflection;
using HotChocolate.ApolloFederation;
using HotChocolate.Data;
using Balance.API.Data;
using BalanceModel = Balance.API.Models.Balance;
using Microsoft.EntityFrameworkCore;

namespace Balance.API.GraphQL.Types;

public class BalanceType : ObjectType<BalanceModel>
{
    protected override void Configure(IObjectTypeDescriptor<BalanceModel> descriptor)
    {
        var method = typeof(BalanceType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("balanceID").ResolveReferenceWith(method);

        descriptor.Field(t => t.AssetId).IsProjected(true);

        descriptor.Field("asset")
            .Resolve(ctx =>
            {
                var parent = ctx.Parent<BalanceModel>();
                return new AssetRef { AssetID = parent.AssetId };
            });
    }

    [ReferenceResolver]
    public static async Task<BalanceModel?> GetByIdAsync(
        int id,
        BalanceByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(id);
    }
}

[ObjectType("Asset")]
[HotChocolate.ApolloFederation.Types.Key("assetID")]
public class AssetRef
{
    [HotChocolate.ApolloFederation.Resolvers.ReferenceResolver]
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
