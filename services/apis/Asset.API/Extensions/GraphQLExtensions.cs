using Microsoft.Extensions.DependencyInjection;
using HotChocolate.Execution;
using HotChocolate.ApolloFederation.Types;
using HotChocolate.Types;
using Asset.API.Graph;
using Asset.API.Graph.Types;
using Shared.Temporal;
using Shared.Extensions;

namespace Asset.API.Extensions;

public static class GraphQLExtensions
{
    public static IServiceCollection AddAssetGraphQL(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddApolloFederation()
            .AddQueryType<Query>()
            .AddAutoScaffoldedTypes(typeof(Query).Assembly)
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddHttpRequestInterceptor<TemporalHttpRequestInterceptor>();

        return services;
    }
}
