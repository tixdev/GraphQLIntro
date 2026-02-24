using Microsoft.Extensions.DependencyInjection;
using HotChocolate.Execution;
using HotChocolate.ApolloFederation.Types;
using HotChocolate.Types;
using Balance.API.Graph;
using Balance.API.Graph.Types;

namespace Balance.API.Extensions;

public static class GraphQLExtensions
{
    public static IServiceCollection AddBalanceGraphQL(this IServiceCollection services)
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
            .AddSorting();

        return services;
    }
}
