using Microsoft.Extensions.DependencyInjection;
using HotChocolate.Execution;
using HotChocolate.ApolloFederation.Types;
using Person.API.Graph;
using Person.API.Graph.Types;
using Shared.Temporal;
using Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Person.API.Data;
using HotChocolate.Execution.Configuration;

namespace Person.API.Extensions;

public static class GraphQLExtensions
{
    public static IServiceCollection AddPersonGraphQL(this IServiceCollection services)
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
                        person(take: 1, order: { personID: ASC }) {
                            items {
                                personID
                                personNumber
                            }
                        }
                    }", ct);

                if (result is OperationResult { Errors: { Count: > 0 } errors })
                {
                    var errorMessages = string.Join("\n\n", errors.Select(e => e.Exception != null ? e.Exception.ToString() : e.Message));
                    throw new Exception($"[Warmup Failed] GraphQL errors:\n{errorMessages}");
                }
            })
            .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true);

        return services;
    }
}
