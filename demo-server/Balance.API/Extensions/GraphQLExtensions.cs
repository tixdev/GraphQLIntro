using Microsoft.Extensions.DependencyInjection;
using HotChocolate.Execution;
using HotChocolate.ApolloFederation.Types;
using HotChocolate.Types;
using BalanceAPI.GraphQL;
using BalanceAPI.GraphQL.Types;

namespace BalanceAPI.Extensions;

public static class GraphQLExtensions
{
    public static IServiceCollection AddBalanceGraphQL(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddApolloFederation()
            .AddQueryType<Query>()
            .AddType<BalanceType>()
            .AddTypeExtension(new ObjectTypeExtension(d => d
                .Name("CollectionSegmentInfo")
                .Shareable()))
            .AddProjections()
            .AddFiltering()
            .AddSorting();

        return services;
    }
}
