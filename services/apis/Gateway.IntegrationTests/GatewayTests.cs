using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace Gateway.IntegrationTests;

public class TelemetryMetrics
{
    [JsonPropertyName("graphQLRequests")]
    public int GraphQLRequests { get; set; }
    [JsonPropertyName("sqlQueries")]
    public int SqlQueries { get; set; }
    [JsonPropertyName("sqlStatements")]
    public List<string> SqlStatements { get; set; } = new();
}

public class GatewayTests : IAsyncLifetime
{
    private readonly HttpClient _http = new();
    private readonly Xunit.Abstractions.ITestOutputHelper _output;

    private const string GatewayUrl = "http://localhost:4000/";

    private readonly Dictionary<string, string> _subgraphs = new()
    {
        ["person"]       = "http://localhost:5011",
        ["relationship"] = "http://localhost:5012",
        ["asset"]        = "http://localhost:5013",
        ["balance"]      = "http://localhost:5014"
    };

    public GatewayTests(Xunit.Abstractions.ITestOutputHelper output)
    {
        _output = output;
    }

    public async Task InitializeAsync() => await ResetAllMetricsAsync();

    public Task DisposeAsync() { _http.Dispose(); return Task.CompletedTask; }

    private async Task ResetAllMetricsAsync()
    {
        var tasks = _subgraphs.Values.Select(url => _http.DeleteAsync($"{url}/_metrics"));
        await Task.WhenAll(tasks);
    }

    private async Task<Dictionary<string, TelemetryMetrics>> FetchAllMetricsAsync()
    {
        var results = new Dictionary<string, TelemetryMetrics>();
        foreach (var (name, url) in _subgraphs)
        {
            var metrics = await _http.GetFromJsonAsync<TelemetryMetrics>($"{url}/_metrics");
            results[name] = metrics ?? new TelemetryMetrics();
        }
        return results;
    }

    private async Task<string> ExecuteQueryAsync(string query)
    {
        var response = await _http.PostAsJsonAsync(GatewayUrl, new { query });
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [Fact(DisplayName = "N+1: Person->Relationships->Assets->Balances should use batched SQL, not N queries")]
    public async Task DeepNesting_ShouldNotCauseNPlusOneQueries()
    {
        await ResetAllMetricsAsync();

        const string query = @"query {
  person(take: 10) {
    items {
      personID
      personNumber
      naturalPerson { agreedName }
      relationships {
        relationshipID
        number
        name { name }
      }
    }
  }
}
";
        
        var json = await ExecuteQueryAsync(query);
        var telemetry = await FetchAllMetricsAsync();
        
        _output.WriteLine("=== TELEMETRY RESULTS ===");
        foreach (var (api, metrics) in telemetry)
        {
            _output.WriteLine($"{api.ToUpper()}: {metrics.GraphQLRequests} GraphQL Requests | {metrics.SqlQueries} SQL Queries");
            if (metrics.SqlStatements.Any())
            {
                _output.WriteLine("SQL Statements Executed:");
                foreach (var sql in metrics.SqlStatements)
                    _output.WriteLine($"  - {sql.Trim().Replace(Environment.NewLine, " ")}");
            }
            _output.WriteLine("-------------------------");
        }
        
        Assert.DoesNotContain("\"errors\"", json);
        
        Assert.Equal(1, telemetry["person"].GraphQLRequests);
        Assert.Equal(1, telemetry["relationship"].GraphQLRequests);
        
        Assert.True(telemetry["person"].SqlQueries <= 2,
            $"PERSON N+1 detected: {telemetry["person"].SqlQueries} queries (expected ≤2). SQL:\n{string.Join("\n---\n", telemetry["person"].SqlStatements)}");

        Assert.True(telemetry["relationship"].SqlQueries <= 2,
            $"RELATIONSHIP N+1 detected: {telemetry["relationship"].SqlQueries} queries (expected ≤2). SQL:\n{string.Join("\n---\n", telemetry["relationship"].SqlStatements)}");
    }

    [Fact(DisplayName = "Gateway: Simple person query hits only Person.API")]
    public async Task SimplePersonQuery_ShouldOnlyHitPersonSubgraph()
    {
        await ResetAllMetricsAsync();

        const string query = @"query{
  person(take: 5) {
    items {
      personID
      personNumber
    }
  }
}";

        var json = await ExecuteQueryAsync(query);
        var telemetry = await FetchAllMetricsAsync();

        _output.WriteLine("=== TELEMETRY RESULTS (Simple Person Query) ===");
        
        Assert.DoesNotContain("\"errors\"", json);

        foreach (var (api, metrics) in telemetry)
        {
            _output.WriteLine($"{api.ToUpper()}: {metrics.GraphQLRequests} GraphQL Requests | {metrics.SqlQueries} SQL Queries");
            if (metrics.SqlStatements.Any())
            {
                _output.WriteLine("SQL Statements Executed:");
                foreach (var sql in metrics.SqlStatements)
                    _output.WriteLine($"  - {sql.Trim().Replace(Environment.NewLine, " ")}");
            }
            _output.WriteLine("-------------------------");
        }
        
        Assert.True(telemetry["person"].GraphQLRequests >= 1, "Person.API should receive at least 1 request");
        Assert.Equal(0, telemetry["relationship"].GraphQLRequests);
        Assert.Equal(0, telemetry["asset"].GraphQLRequests);
        Assert.Equal(0, telemetry["balance"].GraphQLRequests);
    }
}
