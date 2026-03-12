using Product.API.Graph.DataLoaders;
using Product.API.Models;

namespace Product.API.Graph.Resolvers;

public class ProductGroupResolvers
{
    public async Task<IEnumerable<ProductGroupDescription>> GetProductGroupDescriptionAsync(
        [Parent] ProductGroup productGroup,
        [Service] ProductGroupDescriptionByGroupIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        return await dataLoader.LoadAsync(productGroup.ProductGroupId, cancellationToken) ?? Array.Empty<ProductGroupDescription>();
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<ProductGroupDetail>> GetProductGroupDetailAsync(
        [Parent] ProductGroup productGroup,
        [Service] ProductGroupDetailByGroupIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        return await dataLoader.LoadAsync(productGroup.ProductGroupId, cancellationToken) ?? Array.Empty<ProductGroupDetail>();
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<ProductGroupLifeCycleStatus>> GetProductGroupLifeCycleStatusAsync(
        [Parent] ProductGroup productGroup,
        [Service] ProductGroupLifeCycleStatusByGroupIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        return await dataLoader.LoadAsync(productGroup.ProductGroupId, cancellationToken) ?? Array.Empty<ProductGroupLifeCycleStatus>();
    }
}
