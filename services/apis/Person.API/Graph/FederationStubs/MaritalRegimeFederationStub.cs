using HotChocolate;
using HotChocolate.ApolloFederation.Types;
namespace Person.API.Graph.FederationStubs;

[GraphQLName("MaritalRegime")]
public class MaritalRegimeFederationStub
{
    [Key]
    public int MaritalRegimeID { get; set; }
}
