using HotChocolate;
using HotChocolate.ApolloFederation.Types;
namespace Person.API.Graph.FederationStubs;

[GraphQLName("PersonDependentsNumber")]
public class PersonDependentsNumberFederationStub
{
    [Key]
    public int PersonDependentsNumberID { get; set; }
}
