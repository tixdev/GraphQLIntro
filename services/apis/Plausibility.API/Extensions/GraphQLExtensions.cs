using Microsoft.Extensions.DependencyInjection;
using HotChocolate.Execution;
using HotChocolate.ApolloFederation.Types;
using Plausibility.API.Graph.Queries;
using Shared.Temporal;
using System;
using System.Linq;
using HotChocolate.Types;

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
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddAutoScaffoldedTypes()
            .AddTypeExtension(new ObjectTypeExtension(d => d
                .Name("CollectionSegmentInfo")
                .Shareable()))
            .AddHttpRequestInterceptor<TemporalHttpRequestInterceptor>()
            .InitializeOnStartup(warmup: async (executor, ct) =>
            {
                var result = await executor.ExecuteAsync(@"
                    query Warmup {
                        genderCodes(take: 10) {
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
