using HotChocolate;
using HotChocolate.ApolloFederation.Types;
namespace Person.API.Graph.FederationStubs;

[GraphQLName("PersonOrganizationType")]
public class PersonOrganizationTypeFederationStub
{
    [Key]
    public int PersonOrganizationTypeID { get; set; }
}
