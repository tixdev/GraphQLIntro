using HotChocolate;
using HotChocolate.ApolloFederation.Types;
namespace Relationship.API.Graph.FederationStubs;

[GraphQLName("RelationshipType")]
public class RelationshipTypeFederationStub
{
    [Key]
    public int RelationshipTypeID { get; set; }
}
