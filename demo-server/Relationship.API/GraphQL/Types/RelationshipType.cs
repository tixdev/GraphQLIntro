using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using RelationshipModel = Relationship.API.Models.Relationship;
using Relationship.API.Models;

namespace Relationship.API.GraphQL.Types;

public class RelationshipType : ObjectType<RelationshipModel>
{
    protected override void Configure(IObjectTypeDescriptor<RelationshipModel> descriptor)
    {
        var method = typeof(RelationshipType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("relationshipID").ResolveReferenceWith(method);

        // We don't expose PersonId directly anymore as it is in RelationshipToPerson
        // The Person -> Relationship direction is handled in PersonRef.

        descriptor.Field(t => t.Name).Type<RelationshipNameType>();
    }

    [ReferenceResolver]
    public static async Task<RelationshipModel?> GetByIdAsync(
        int id,
        RelationshipByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(id);
    }
}

public class RelationshipNameType : ObjectType<RelationshipName>
{
    protected override void Configure(IObjectTypeDescriptor<RelationshipName> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(t => t.Name);
    }
}

[ObjectType("Person")]
[Key("personID")]
public class PersonRef
{
    [ReferenceResolver]
    public static async Task<PersonRef> GetByIdAsync(int personID) =>
        await Task.FromResult(new PersonRef { PersonID = personID });

    public int PersonID { get; set; }

    public async Task<RelationshipModel[]> GetRelationships([Parent] PersonRef person,
        RelationshipsByPersonIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(person.PersonID);
    }
}