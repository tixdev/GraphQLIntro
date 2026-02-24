using Microsoft.Extensions.DependencyInjection;
using HotChocolate.Execution;
using HotChocolate.ApolloFederation.Types;
using HotChocolate.Types;
using Asset.API.Graph;
using Asset.API.Graph.Types;

namespace Asset.API.Extensions;

public static class GraphQLExtensions
{
    public static IServiceCollection AddAssetGraphQL(this IServiceCollection services)
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
