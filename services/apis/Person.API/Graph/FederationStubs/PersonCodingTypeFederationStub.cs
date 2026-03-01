using HotChocolate;
using HotChocolate.ApolloFederation.Types;
namespace Person.API.Graph.FederationStubs;

[GraphQLName("PersonCodingType")]
public class PersonCodingTypeFederationStub
{
    [Key]
    public int PersonCodingTypeID { get; set; }
}
