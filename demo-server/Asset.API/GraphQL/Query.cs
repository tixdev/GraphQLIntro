using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using Microsoft.EntityFrameworkCore;
using AssetAPI.Data;
using AssetModel = AssetAPI.Models.Asset;

namespace AssetAPI.GraphQL;

public class Query
{
    [UseOffsetPaging(MaxPageSize = 200, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<AssetModel> GetAsset([Service] AssetContext context) => context.Assets.AsNoTracking();
}
