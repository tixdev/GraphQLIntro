using Relationship.API.Models;
using Relationship.API.Graph.FederationStubs;

namespace Relationship.API.Graph.TypeExtensions;

[ExtendObjectType(typeof(RelationshipToPerson))]
public class RelationshipToPersonPlausibilityExtensions
{
    public PersonToRelationshipRoleFederationStub? GetPersonToRelationshipRole([Parent] RelationshipToPerson model)
        => model.PltPersonToRelationshipRoleID != 0
            ? new PersonToRelationshipRoleFederationStub
                { PersonToRelationshipRoleID = model.PltPersonToRelationshipRoleID }
            : null;
}

[ExtendObjectType(typeof(Relationship.API.Models.Relationship))]
public class RelationshipPlausibilityExtensions
{
    public RelationshipTypeFederationStub? GetRelationshipType([Parent] Relationship.API.Models.Relationship model)
        => model.PltRelationshipTypeID != 0
            ? new RelationshipTypeFederationStub { RelationshipTypeID = model.PltRelationshipTypeID }
            : null;
}