using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Relationship.API.Data;

public class MetricsDbCommandInterceptor(TestMetrics metrics) : DbCommandInterceptor
{
    public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
        DbCommand command, CommandEventData eventData,
        InterceptionResult<DbDataReader> result,
        CancellationToken cancellationToken = default)
    {
        Track(command);
        return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
    }

    public override ValueTask<InterceptionResult<object>> ScalarExecutingAsync(
        DbCommand command, CommandEventData eventData,
        InterceptionResult<object> result,
        CancellationToken cancellationToken = default)
    {
        Track(command);
        return base.ScalarExecutingAsync(command, eventData, result, cancellationToken);
    }

    private void Track(DbCommand command)
    {
        var sql = command.CommandText;
        // Skip EF Core system-level check queries
        if (sql.Contains("sys.objects") || sql.Contains("IS_SRVROLEMEMBER")
            || sql.Contains("SELECT 1") || sql.Contains("CASE") && sql.Contains("EXISTS"))
            return;
        metrics.SqlQueries++;
        metrics.SqlStatements.Add(sql);
    }
}
