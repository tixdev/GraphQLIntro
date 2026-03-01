using HotChocolate;
using HotChocolate.ApolloFederation.Types;
namespace Person.API.Graph.FederationStubs;

[GraphQLName("PersonAcquisitionSource")]
public class PersonAcquisitionSourceFederationStub
{
    [Key]
    public int PersonAcquisitionSourceID { get; set; }
}
