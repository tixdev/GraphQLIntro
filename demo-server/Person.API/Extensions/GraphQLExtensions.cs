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
            .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true);

        return services;
    }
}
