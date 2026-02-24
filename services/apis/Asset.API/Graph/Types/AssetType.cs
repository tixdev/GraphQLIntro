using HotChocolate.Types;
using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using HotChocolate.Data;
using System.Reflection;
using HotChocolate.ApolloFederation;
using Asset.API.Data;
using AssetModel = Asset.API.Models.Asset;
using Microsoft.EntityFrameworkCore;
using Asset.API.Graph.DataLoaders;
using Asset.API.Graph.Extensions;

namespace Asset.API.Graph.Types;

public class AssetType : ObjectType<AssetModel>
{
    protected override void Configure(IObjectTypeDescriptor<AssetModel> descriptor)
    {
        var method = typeof(AssetType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("assetID").ResolveReferenceWith(method);

        descriptor.Field(t => t.RelationshipId).IsProjected(true);

        descriptor.Field("relationship")
            .Resolve(ctx =>
            {
                var parent = ctx.Parent<AssetModel>();
                return new RelationshipRef { RelationshipID = parent.RelationshipId };
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
