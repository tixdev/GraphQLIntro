using HotChocolate;
using HotChocolate.ApolloFederation.Types;

namespace Person.API.Graph.FederationStubs;

[GraphQLName("PersonNameType")]
public class PersonNameTypeFederationStub
{
    [Key]
    public int PersonNameTypeID { get; set; }
}
