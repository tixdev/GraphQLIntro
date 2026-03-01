using HotChocolate;
using HotChocolate.ApolloFederation.Types;
namespace Person.API.Graph.FederationStubs;

[GraphQLName("Pep")]
public class PepFederationStub
{
    [Key]
    public int PepID { get; set; }
}
