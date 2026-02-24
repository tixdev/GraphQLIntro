using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using Microsoft.EntityFrameworkCore;
using Asset.API.Data;
using AssetModel = Asset.API.Models.Asset;

namespace Asset.API.Graph;

public class Query
{
    [UseOffsetPaging(MaxPageSize = 200, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<AssetModel> GetAsset([Service] AssetContext context) => context.Assets.AsNoTracking();
}
