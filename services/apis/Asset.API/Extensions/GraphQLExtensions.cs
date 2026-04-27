using HotChocolate.Execution;
using Asset.API.Graph;
using Shared.Temporal;
using Shared.Extensions;

namespace Asset.API.Extensions;

public static class GraphQLExtensions
{
    public static IServiceCollection AddAssetGraphQL(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddApolloFederation()
            .AddQueryType<Query>()
            .AddAutoScaffoldedTypes(typeof(Query).Assembly)
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddInstrumentation()
            .AddHttpRequestInterceptor<TemporalHttpRequestInterceptor>()
            .AddWarmupTask(async (executor, ct) =>
            {
                var result = await executor.ExecuteAsync(@"
                    query Warmup {
                        asset(take: 1, order: { assetID: ASC }) {
                            items {
                                assetID
                            }
                        }
                    }", ct);

                if (result is OperationResult { Errors: { Count: > 0 } errors })
                {
                    var errorMessages = string.Join(", ", errors.Select(e => e.Message));
                    throw new Exception($"[Warmup Failed] GraphQL errors: {errorMessages}");
                }
            })
            .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true);

        return services;
    }
}
