using HotChocolate.Types;
using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using HotChocolate.Data;
using System.Reflection;
using HotChocolate.ApolloFederation;
using Balance.API.Data;
using BalanceModel = Balance.API.Models.Balance;
using Microsoft.EntityFrameworkCore;
using Balance.API.Graph.DataLoaders;
using Balance.API.Graph.ExternalTypeRefs;

namespace Balance.API.Graph.Types;

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
