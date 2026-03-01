using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.DataLoaders;

public class EmployeesRangeNumberByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PlausibilityDbContext dbContext)
    : BatchDataLoader<int, EmployeesRangeNumber>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, EmployeesRangeNumber>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.EmployeesRangeNumbers
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(p => keys.Contains(p.EmployeesRangeNumberID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.EmployeesRangeNumberID);
    }
}
