using HotChocolate;
using HotChocolate.ApolloFederation.Types;
namespace Person.API.Graph.FederationStubs;

[GraphQLName("PersonMaritalStatus")]
public class PersonMaritalStatusFederationStub
{
    [Key]
    public int PersonMaritalStatusID { get; set; }
}
