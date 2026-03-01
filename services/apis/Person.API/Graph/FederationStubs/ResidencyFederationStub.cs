using HotChocolate;
using HotChocolate.ApolloFederation.Types;
namespace Person.API.Graph.FederationStubs;

[GraphQLName("Residency")]
public class ResidencyFederationStub
{
    [Key]
    public int ResidencyID { get; set; }
}
