using Product.API.Graph.DataLoaders;
using Product.API.Models;

namespace Product.API.Graph.Resolvers;

public class ProductResolvers
{
    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<ProductDescription>> GetProductDescriptionAsync(
        [Parent] Models.Product product,
        [Service] ProductDescriptionByProductIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        return await dataLoader.LoadAsync(product.ProductId, cancellationToken) ?? Array.Empty<ProductDescription>();
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<ProductDetail>> GetProductDetailAsync(
        [Parent] Models.Product product,
        [Service] ProductDetailByProductIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        return await dataLoader.LoadAsync(product.ProductId, cancellationToken) ?? Array.Empty<ProductDetail>();
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<ProductGroupToProduct>> GetProductGroupToProductAsync(
        [Parent] Models.Product product,
        [Service] ProductGroupToProductByProductIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        return await dataLoader.LoadAsync(product.ProductId, cancellationToken) ?? Array.Empty<ProductGroupToProduct>();
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<ProductToProduct>> GetProductToProductAsync(
        [Parent] Models.Product product,
        [Service] ProductToProductByProductIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        return await dataLoader.LoadAsync(product.ProductId, cancellationToken) ?? Array.Empty<ProductToProduct>();
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<ProductDocument>> GetProductDocumentAsync(
        [Parent] Models.Product product,
        [Service] ProductDocumentByProductIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        return await dataLoader.LoadAsync(product.ProductId, cancellationToken) ?? Array.Empty<ProductDocument>();
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<ProductRoleAllowed>> GetProductRoleAllowedAsync(
        [Parent] Models.Product product,
        [Service] ProductRoleAllowedByProductIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        return await dataLoader.LoadAsync(product.ProductId, cancellationToken) ?? Array.Empty<ProductRoleAllowed>();
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<ProductToCondition>> GetProductToConditionAsync(
        [Parent] Models.Product product,
        [Service] ProductToConditionByProductIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        return await dataLoader.LoadAsync(product.ProductId, cancellationToken) ?? Array.Empty<ProductToCondition>();
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<ProductLifeCycleStatus>> GetProductLifeCycleStatusAsync(
        [Parent] Models.Product product,
        [Service] ProductLifeCycleStatusByProductIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        return await dataLoader.LoadAsync(product.ProductId, cancellationToken) ?? Array.Empty<ProductLifeCycleStatus>();
    }
}
