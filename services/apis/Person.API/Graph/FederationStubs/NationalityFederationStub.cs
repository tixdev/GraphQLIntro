using HotChocolate;
using HotChocolate.ApolloFederation.Types;
namespace Person.API.Graph.FederationStubs;

[GraphQLName("Nationality")]
public class NationalityFederationStub
{
    [Key]
    public int NationalityID { get; set; }
}
