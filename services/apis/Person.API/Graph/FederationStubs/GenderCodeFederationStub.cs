using HotChocolate;
using HotChocolate.ApolloFederation.Types;
namespace Person.API.Graph.FederationStubs;

[GraphQLName("GenderCode")]
public class GenderCodeFederationStub
{
    [Key]
    public int GenderCodeID { get; set; }
}
