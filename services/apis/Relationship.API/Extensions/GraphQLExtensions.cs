using Microsoft.Extensions.DependencyInjection;
using HotChocolate.Execution;
using HotChocolate.ApolloFederation.Types;
using Relationship.API.Graph;
using Relationship.API.Graph.Types;
using Relationship.API.Graph.Extensions;

namespace Relationship.API.Extensions;

public static class GraphQLExtensions
{
    public static IServiceCollection AddRelationshipGraphQL(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddApolloFederation()
            .AddQueryType<Query>()
            .AddAutoScaffoldedTypes()
            .AddTypeExtension(new ObjectTypeExtension(d => d
                .Name("CollectionSegmentInfo")
                .Shareable()))
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .InitializeOnStartup(warmup: async (executor, ct) =>
            {
                var result = await executor.ExecuteAsync(@"
                    query Warmup {
                        relationship(take: 10) {
                            items {
                                relationshipID
                                number
                            }
                        }
                    }", ct);

                if (result is IOperationResult { Errors: { Count: > 0 } errors })
                {
                    var errorMessages = string.Join(", ", errors.Select(e => e.Message));
                    throw new Exception($"[Warmup Failed] GraphQL errors: {errorMessages}");
                }
            })
            .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true);

        return services;
    }
}
