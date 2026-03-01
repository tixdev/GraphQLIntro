using HotChocolate;
using HotChocolate.ApolloFederation.Types;
namespace Relationship.API.Graph.FederationStubs;

[GraphQLName("PersonToRelationshipRole")]
public class PersonToRelationshipRoleFederationStub
{
    [Key]
    public int PersonToRelationshipRoleID { get; set; }
}
