using HotChocolate.Execution;
using Plausibility.API.Graph.Queries;
using Shared.Temporal;
using Shared.Extensions;

namespace Plausibility.API.Extensions;

public static class GraphQLExtensions
{
    public static IServiceCollection AddPlausibilityGraphQL(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .ModifyOptions(options =>
            {
                options.DefaultBindingBehavior = BindingBehavior.Implicit;
            })
            .AddApolloFederation()
            .AddQueryType<PlausibilityQueries>()
            .AddAutoScaffoldedTypes(typeof(PlausibilityQueries).Assembly)
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddHttpRequestInterceptor<TemporalHttpRequestInterceptor>()
            .InitializeOnStartup(warmup: async (executor, ct) =>
            {
                var result = await executor.ExecuteAsync(@"
                    query Warmup {
                        genderCodes(take: 1, order: { genderCodeID: ASC }) {
                            items {
                                genderCodeID
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
