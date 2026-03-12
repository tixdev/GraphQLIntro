using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Product.API.Data;

public class MetricsDbCommandInterceptor : DbCommandInterceptor
{
    private readonly TestMetrics _metrics;

    public MetricsDbCommandInterceptor(TestMetrics metrics) => _metrics = metrics;

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
        _metrics.SqlQueries++;
        _metrics.SqlStatements.Add(sql);
    }
}
