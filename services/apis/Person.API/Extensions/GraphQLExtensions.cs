using Microsoft.Extensions.DependencyInjection;
using HotChocolate.Execution;
using HotChocolate.ApolloFederation.Types;
using Person.API.GraphQL;
using Person.API.GraphQL.Types;

namespace Person.API.Extensions;

public static class GraphQLExtensions
{
    public static IServiceCollection AddPersonGraphQL(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddApolloFederation()
            .AddQueryType<Query>()
            .AddTypeExtension<PersonType>()
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
                        person(take: 10) {
                            items {
                                personID
                                personNumber
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
