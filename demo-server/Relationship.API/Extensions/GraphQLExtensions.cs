using HotChocolate.ApolloFederation.Types;
using Relationship.API.GraphQL;
using Relationship.API.GraphQL.Types;

namespace Relationship.API.Extensions;

public static class GraphQLExtensions
{
    public static IServiceCollection AddRelationshipGraphQL(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddApolloFederation()
            .AddQueryType<Query>()
            .AddType<RelationshipType>()
            .AddType<PersonRef>()
            .AddTypeExtension(new ObjectTypeExtension(d => d
                .Name("CollectionSegmentInfo")
                .Shareable()))
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true);

        return services;
    }
}
