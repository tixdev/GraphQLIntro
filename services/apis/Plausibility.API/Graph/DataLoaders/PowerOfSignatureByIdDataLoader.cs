using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.DataLoaders;

public class PowerOfSignatureByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    PlausibilityDbContext dbContext)
    : BatchDataLoader<int, PowerOfSignature>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<int, PowerOfSignature>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var items = await dbContext.PowerOfSignatures
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(p => keys.Contains(p.PowerOfSignatureID))
            .ToListAsync(cancellationToken);

        return items.ToDictionary(p => p.PowerOfSignatureID);
    }
}
