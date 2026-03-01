using HotChocolate;
using HotChocolate.ApolloFederation.Types;
namespace Person.API.Graph.FederationStubs;

[GraphQLName("Noga")]
public class NogaFederationStub
{
    [Key]
    public int NogaID { get; set; }
}
