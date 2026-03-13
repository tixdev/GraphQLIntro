using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Asset.API.Graph.DataLoaders;
using AssetModel = Asset.API.Models.Asset;

namespace Asset.API.Graph.ExternalTypeRefs;

[ObjectType("Product")]
[Key("productId")]
[GraphQLName("Product")]
public class ProductRef
{
    [ReferenceResolver]
    public static async Task<ProductRef> GetByIdAsync(int productId)
        => await Task.FromResult(new ProductRef { ProductId = productId });

    [ID]
    public int ProductId { get; set; }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<AssetModel>> GetAssets(
        [Parent] ProductRef product,
        AssetsByProductIdDataLoader dataLoader)
    {
        var results = await dataLoader.LoadAsync(product.ProductId);
        return results ?? Array.Empty<AssetModel>();
    }
}
