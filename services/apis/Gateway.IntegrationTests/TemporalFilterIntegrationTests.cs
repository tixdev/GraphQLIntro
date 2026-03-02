using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Nodes;

namespace Gateway.IntegrationTests;

public class TemporalFilterIntegrationTests : IAsyncLifetime
{
    private readonly HttpClient _http = new();
    private readonly Xunit.Abstractions.ITestOutputHelper _output;

    private const string GatewayUrl = "http://localhost:4000/";

    public TemporalFilterIntegrationTests(Xunit.Abstractions.ITestOutputHelper output)
    {
        _output = output;
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() { _http.Dispose(); return Task.CompletedTask; }

    private async Task<string> ExecuteQueryWithHeadersAsync(string query, Dictionary<string, string>? headers = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, GatewayUrl)
        {
            Content = JsonContent.Create(new { query })
        };

        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }

        var response = await _http.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Request failed with status {response.StatusCode}: {errorContent}");
        }
        return await response.Content.ReadAsStringAsync();
    }

    private void CheckDatesActivelyBetween(JsonNode? personTypeNode, DateTime rangeStart, DateTime rangeEnd)
    {
        if (personTypeNode == null) return;
        
        var startStr = personTypeNode["validStartDate"]?.GetValue<string>();
        var endStr = personTypeNode["validEndDate"]?.GetValue<string>();
        
        if (DateTime.TryParse(startStr, out var validStart) && DateTime.TryParse(endStr, out var validEnd))
        {
            Assert.True(validStart <= rangeEnd, $"ValidStartDate {validStart:O} should be <= RangeEnd {rangeEnd:O}");
            Assert.True(validEnd >= rangeStart, $"ValidEndDate {validEnd:O} should be >= RangeStart {rangeStart:O}");
        }
    }

    private void CheckDatesAsOf(JsonNode? personTypeNode, DateTime asOfDate)
    {
        if (personTypeNode == null) return;
        
        var startStr = personTypeNode["validStartDate"]?.GetValue<string>();
        var endStr = personTypeNode["validEndDate"]?.GetValue<string>();
        
        if (DateTime.TryParse(startStr, out var validStart) && DateTime.TryParse(endStr, out var validEnd))
        {
            // Adding a small margin since test initialization might differ by milliseconds from the server evaluation
            Assert.True(validStart <= asOfDate.AddMinutes(5), $"ValidStartDate {validStart:O} should be <= AsOfDate {asOfDate:O}");
            Assert.True(validEnd >= asOfDate.AddMinutes(-5), $"ValidEndDate {validEnd:O} should be >= AsOfDate {asOfDate:O}");
        }
    }

    [Fact(DisplayName = "Temporal: Default mode returns dates coherent with today")]
    public async Task Temporal_DefaultMode_DatesShouldBeCoherent()
    {
        const string query = @"query { 
            person(take: 10) { 
                items { 
                    personNumber 
                    groupPerson { validStartDate validEndDate } 
                    internalPerson { validStartDate validEndDate } 
                    legalPerson { validStartDate validEndDate } 
                    naturalPerson { validStartDate validEndDate } 
                } 
            } 
        }";
        
        var json = await ExecuteQueryWithHeadersAsync(query);
        _output.WriteLine(json);
        Assert.DoesNotContain("\"errors\"", json);
        
        var parsed = JsonNode.Parse(json);
        var items = parsed?["data"]?["person"]?["items"]?.AsArray();
        Assert.NotNull(items);
        Assert.NotEmpty(items);

        var today = DateTime.Now;
        foreach (var item in items)
        {
            CheckDatesAsOf(item?["groupPerson"], today);
            CheckDatesAsOf(item?["internalPerson"], today);
            CheckDatesAsOf(item?["legalPerson"], today);
            CheckDatesAsOf(item?["naturalPerson"], today);
        }
    }
    
    [Fact(DisplayName = "Temporal: ActiveBetween returns dates coherent with range")]
    public async Task Temporal_ActiveBetweenMode_DatesShouldBeCoherent()
    {
        const string query = @"query { 
            person(take: 10) { 
                items { 
                    personNumber 
                    groupPerson { validStartDate validEndDate } 
                    internalPerson { validStartDate validEndDate } 
                    legalPerson { validStartDate validEndDate } 
                    naturalPerson { validStartDate validEndDate } 
                } 
            } 
        }";

        var rangeStart = new DateTime(2020, 1, 1);
        var rangeEnd = new DateTime(2025, 12, 31, 23, 59, 59);

        var headers = new Dictionary<string, string>
        {
            { "x-temporal-mode", "ActiveBetween" },
            { "x-temporal-range-start", "2020-01-01T00:00:00" },
            { "x-temporal-range-end", "2025-12-31T23:59:59" }
        };
        
        var json = await ExecuteQueryWithHeadersAsync(query, headers);
        _output.WriteLine(json);
        Assert.DoesNotContain("\"errors\"", json);
        
        var parsed = JsonNode.Parse(json);
        var items = parsed?["data"]?["person"]?["items"]?.AsArray();
        Assert.NotNull(items);
        Assert.NotEmpty(items);

        foreach (var item in items)
        {
            CheckDatesActivelyBetween(item?["groupPerson"], rangeStart, rangeEnd);
            CheckDatesActivelyBetween(item?["internalPerson"], rangeStart, rangeEnd);
            CheckDatesActivelyBetween(item?["legalPerson"], rangeStart, rangeEnd);
            CheckDatesActivelyBetween(item?["naturalPerson"], rangeStart, rangeEnd);
        }
    }

    [Fact(DisplayName = "Temporal: Validation throws error if End is provided without Start")]
    public async Task Temporal_HeaderValidation_EndWithoutStart_ShouldThrow()
    {
        const string query = @"query { person(take: 1) { items { personNumber } } }";
        var headers = new Dictionary<string, string> { { "x-temporal-mode", "ActiveBetween" }, { "x-temporal-range-end", "2025-12-31T23:59:59" } };
        var json = await ExecuteQueryWithHeadersAsync(query, headers);
        Assert.Contains("\"errors\"", json);
    }
    
    [Fact(DisplayName = "Temporal: Validation throws error if End is before Start")]
    public async Task Temporal_HeaderValidation_EndBeforeStart_ShouldThrow()
    {
        const string query = @"query { person(take: 1) { items { personNumber } } }";
        var headers = new Dictionary<string, string> { { "x-temporal-mode", "ActiveBetween" }, { "x-temporal-range-start", "2025-12-31T23:59:59" }, { "x-temporal-range-end", "2020-01-01T00:00:00" } };
        var json = await ExecuteQueryWithHeadersAsync(query, headers);
        Assert.Contains("\"errors\"", json);
    }

    [Fact(DisplayName = "Temporal: All mode should return full history without crashing")]
    public async Task Temporal_AllMode_ShouldNotFail()
    {
        const string query = @"query { person(take: 10) { items { personNumber groupPerson { validStartDate validEndDate } } } }";
        var headers = new Dictionary<string, string> { { "x-temporal-mode", "All" } };
        var json = await ExecuteQueryWithHeadersAsync(query, headers);
        Assert.DoesNotContain("\"errors\"", json);
        Assert.Contains("\"personNumber\"", json);
    }

    [Fact(DisplayName = "Temporal: Nested relationships query should not fail on null properties")]
    public async Task Temporal_NestedRelationships_ShouldNotFail()
    {
        const string query = @"query { person(take: 2) { items { personNumber personNature { code } relationships { items { number persons { personNumber } } } } } }";
        var json = await ExecuteQueryWithHeadersAsync(query);
        Assert.DoesNotContain("\"errors\"", json);
    }
    
    [Fact(DisplayName = "Temporal: ActiveBetween on 010000663 in 2012 should not return 2013 InternalPerson")]
    public async Task Temporal_ActiveBetweenMode_SpecificPersonDoesNotReturnFutureEntities()
    {
        const string query = @"query {
          person(where: { personNumber: { eq: ""010000663"" } }) {
            items {        
              personNumber
              personNature { code }
              groupPerson { validStartDate validEndDate }
              internalPerson { validStartDate validEndDate }
              legalPerson { validStartDate validEndDate }
              naturalPerson { validStartDate validEndDate }
              relationships {
                items { number persons { personNumber } }
              }
            }
          }
        }";

        var headers = new Dictionary<string, string>
        {
            { "x-temporal-mode", "ActiveBetween" },
            { "x-temporal-range-start", "2012-01-01T00:00:00" },
            { "x-temporal-range-end", "2013-01-01T00:00:00" }
        };
        
        var json = await ExecuteQueryWithHeadersAsync(query, headers);
        _output.WriteLine(json);
        Assert.DoesNotContain("\"errors\"", json);
        
        var parsed = JsonNode.Parse(json);
        var items = parsed?["data"]?["person"]?["items"]?.AsArray();
        Assert.NotNull(items);
        Assert.NotEmpty(items);
        
        // InternalPerson started in April 2013, so for range [Jan 2012, Jan 2013] it MUST be null.
        var internalPersonNode = items[0]?["internalPerson"];
        Assert.Null(internalPersonNode);
    }
}
