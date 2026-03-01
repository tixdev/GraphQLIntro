using HotChocolate;
using HotChocolate.ApolloFederation.Types;
namespace Person.API.Graph.FederationStubs;

[GraphQLName("PersonInternalType")]
public class PersonInternalTypeFederationStub
{
    [Key]
    public int PersonInternalTypeID { get; set; }
}
