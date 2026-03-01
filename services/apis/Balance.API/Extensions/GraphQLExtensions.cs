using Microsoft.Extensions.DependencyInjection;
using HotChocolate.Execution;
using HotChocolate.ApolloFederation.Types;
using HotChocolate.Types;
using Balance.API.Graph;
using Balance.API.Graph.Types;
using Shared.Extensions;

namespace Balance.API.Extensions;

public static class GraphQLExtensions
{
    public static IServiceCollection AddBalanceGraphQL(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddApolloFederation()
            .AddQueryType<Query>()
            .AddAutoScaffoldedTypes(typeof(Query).Assembly)
            .AddProjections()
            .AddFiltering()
            .AddSorting();

        return services;
    }
}
