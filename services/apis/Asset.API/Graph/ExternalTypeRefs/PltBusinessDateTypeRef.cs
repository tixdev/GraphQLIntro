using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;

namespace Asset.API.Graph.ExternalTypeRefs;

[ObjectType("PltBusinessDateType")]
[Key("businessDateTypeID")]
[GraphQLName("PltBusinessDateType")]
public class PltBusinessDateTypeRef
{
    [ReferenceResolver]
    public static async Task<PltBusinessDateTypeRef> GetByIdAsync(int businessDateTypeID)
        => await Task.FromResult(new PltBusinessDateTypeRef { BusinessDateTypeID = businessDateTypeID });

    public int BusinessDateTypeID { get; set; }
}
