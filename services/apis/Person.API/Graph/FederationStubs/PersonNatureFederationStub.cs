using HotChocolate;
using HotChocolate.ApolloFederation.Types;
namespace Person.API.Graph.FederationStubs;

[GraphQLName("PersonNature")]
public class PersonNatureFederationStub
{
    [Key]
    public int PersonNatureID { get; set; }
}
