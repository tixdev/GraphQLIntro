using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using Microsoft.EntityFrameworkCore;
using BalanceAPI.Data;
using BalanceModel = BalanceAPI.Models.Balance;

namespace BalanceAPI.GraphQL;

public class Query
{
    [UseOffsetPaging(MaxPageSize = 200, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<BalanceModel> GetBalance([Service] BalanceContext context) => context.Balances.AsNoTracking();
}
