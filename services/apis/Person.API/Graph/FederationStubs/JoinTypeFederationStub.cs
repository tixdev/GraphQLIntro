using HotChocolate;
using HotChocolate.ApolloFederation.Types;
namespace Person.API.Graph.FederationStubs;

[GraphQLName("JoinType")]
public class JoinTypeFederationStub
{
    [Key]
    public int JoinTypeID { get; set; }
}
