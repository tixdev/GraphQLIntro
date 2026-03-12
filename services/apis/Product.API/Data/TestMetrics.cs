using System.Collections.Concurrent;

namespace Product.API.Data;

public class TestMetrics
{
    public int GraphQLRequests { get; set; }
    public int SqlQueries { get; set; }
    public ConcurrentBag<string> SqlStatements { get; } = new();

    public void Reset()
    {
        GraphQLRequests = 0;
        SqlQueries = 0;
        SqlStatements.Clear();
    }
}
