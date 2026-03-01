using Microsoft.Extensions.DependencyInjection;
using HotChocolate.Execution;
using HotChocolate.ApolloFederation.Types;
using Person.API.Graph;
using Person.API.Graph.Types;
using Shared.Temporal;

namespace Person.API.Extensions;

public static class GraphQLExtensions
{
    public static IServiceCollection AddPersonGraphQL(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddApolloFederation()
            .AddQueryType<Query>()
            .AddTypeExtension(new ObjectTypeExtension(d => d
                .Name("CollectionSegmentInfo")
                .Shareable()))
            .AddAutoScaffoldedTypes()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddHttpRequestInterceptor<TemporalHttpRequestInterceptor>()
            .InitializeOnStartup(warmup: async (executor, ct) =>
            {
                var result = await executor.ExecuteAsync(@"
                    query Warmup {
                        person(take: 10) {
                            items {
                                personID
                                personNumber
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
