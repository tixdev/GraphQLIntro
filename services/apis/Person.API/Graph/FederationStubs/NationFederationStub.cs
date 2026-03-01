using HotChocolate;
using HotChocolate.ApolloFederation.Types;
namespace Person.API.Graph.FederationStubs;

[GraphQLName("Nation")]
public class NationFederationStub
{
    [Key]
    public int NationID { get; set; }
}
