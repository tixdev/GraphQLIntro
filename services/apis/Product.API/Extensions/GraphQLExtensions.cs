using Product.API.Data;
using Product.API.Models;
using Product.API.Graph;
using HotChocolate.Execution.Configuration;
using Shared.Temporal;
using HotChocolate.Execution;
using Shared.Extensions;

namespace Product.API.Extensions;

public static class GraphQLExtensions
{
    public static IServiceCollection AddGraphQLServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddGraphQLServer()
            .AddApolloFederation()
            .AddAutoScaffoldedTypes(typeof(Query).Assembly)
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddQueryType<Query>()
            .AddHttpRequestInterceptor<TemporalHttpRequestInterceptor>()
            .InitializeOnStartup(warmup: async (executor, ct) =>
            {
                var result = await executor.ExecuteAsync(@"
                    query Warmup {
                        product(take: 1, order: { productId: ASC }) {
                            items {
                                productId
                                productCode
                            }
                        }
                    }", ct);

                if (result is IOperationResult { Errors: { Count: > 0 } errors })
                {
                    var errorMessages = string.Join("\n\n", errors.Select(e => e.Exception != null ? e.Exception.ToString() : e.Message));
                    throw new Exception($"[Warmup Failed] GraphQL errors:\n{errorMessages}");
                }
            })
            .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true);

        return services;
    }
}
