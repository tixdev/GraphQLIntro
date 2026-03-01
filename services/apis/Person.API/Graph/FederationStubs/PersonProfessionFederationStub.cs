using HotChocolate;
using HotChocolate.ApolloFederation.Types;
namespace Person.API.Graph.FederationStubs;

[GraphQLName("PersonProfession")]
public class PersonProfessionFederationStub
{
    [Key]
    public int PersonProfessionID { get; set; }
}
