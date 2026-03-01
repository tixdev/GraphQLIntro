using HotChocolate;
using HotChocolate.ApolloFederation.Types;
namespace Person.API.Graph.FederationStubs;

[GraphQLName("Personality")]
public class PersonalityFederationStub
{
    [Key]
    public int PersonalityID { get; set; }
}
