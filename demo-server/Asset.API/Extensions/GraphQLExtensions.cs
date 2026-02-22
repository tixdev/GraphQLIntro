using Microsoft.Extensions.DependencyInjection;
using HotChocolate.Execution;
using HotChocolate.ApolloFederation.Types;
using HotChocolate.Types;
using Asset.API.GraphQL;
using Asset.API.GraphQL.Types;

namespace Asset.API.Extensions;

public static class GraphQLExtensions
{
    public static IServiceCollection AddAssetGraphQL(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddApolloFederation()
            .AddQueryType<Query>()
            .AddType<AssetType>()
            .AddTypeExtension(new ObjectTypeExtension(d => d
                .Name("CollectionSegmentInfo")
                .Shareable()))
            .AddProjections()
            .AddFiltering()
            .AddSorting();

        return services;
    }
}
