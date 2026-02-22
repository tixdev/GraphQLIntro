using HotChocolate.Types;
using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using HotChocolate.Data;
using System.Reflection;
using HotChocolate.ApolloFederation;
using HotChocolate.Data;
using AssetAPI.Data;
using AssetModel = AssetAPI.Models.Asset;
using Microsoft.EntityFrameworkCore;

namespace AssetAPI.GraphQL.Types;

public class AssetType : ObjectType<AssetModel>
{
    protected override void Configure(IObjectTypeDescriptor<AssetModel> descriptor)
    {
        var method = typeof(AssetType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("id").ResolveReferenceWith(method);

        descriptor.Field(t => t.RelationshipId).IsProjected(true);

        descriptor.Field("relationship")
            .Resolve(ctx =>
            {
                var parent = ctx.Parent<AssetModel>();
                return new RelationshipRef { Id = parent.RelationshipId };
            });
    }

    [ReferenceResolver]
    public static async Task<AssetModel?> GetByIdAsync(
        int id,
        [Service] AssetContext ctx)
    {
        return await ctx.Assets.FirstOrDefaultAsync(a => a.Id == id);
    }
}

[ObjectType("Relationship")]
[HotChocolate.ApolloFederation.Types.Key("id")]
public class RelationshipRef
{
    [HotChocolate.ApolloFederation.Resolvers.ReferenceResolver]
    public static async Task<RelationshipRef> GetByIdAsync(int id)
        => await Task.FromResult(new RelationshipRef { Id = id });
    
    public int Id { get; set; }

    public async Task<List<AssetModel>> GetAssets(
        [Parent] RelationshipRef relationship,
        [Service] AssetContext ctx)
        => await ctx.Assets
            .Where(a => a.RelationshipId == relationship.Id)
            .ToListAsync();
}
