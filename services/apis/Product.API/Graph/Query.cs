using HotChocolate.Data;
using Microsoft.EntityFrameworkCore;
using Product.API.Data;
using Product.API.Models;

namespace Product.API.Graph;

public class Query
{
    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 256, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Models.Product> GetProduct([Service] ProductDbContext dbContext) => dbContext.Product.AsNoTracking();

    public async Task<Models.Product?> GetProductByIdAsync(
        int productId,
        [Service] DataLoaders.ProductByIdDataLoader dataLoader,
        CancellationToken cancellationToken)
        => await dataLoader.LoadAsync(productId, cancellationToken);
}
