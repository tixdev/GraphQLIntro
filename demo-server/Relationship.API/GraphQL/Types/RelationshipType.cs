using HotChocolate.Types;
using HotChocolate.ApolloFederation.Types; // Needed for Key extension
using HotChocolate.ApolloFederation.Resolvers;
using HotChocolate.Data;
using System.Reflection;
using HotChocolate.ApolloFederation;
using HotChocolate.Data;
using Relationship.API.Data;
using RelationshipModel = Relationship.API.Models.Relationship;
using Microsoft.EntityFrameworkCore;

namespace Relationship.API.GraphQL.Types;

public class RelationshipType : ObjectType<RelationshipModel>
{
    protected override void Configure(IObjectTypeDescriptor<RelationshipModel> descriptor)
    {
        var method = typeof(RelationshipType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("relationshipID").ResolveReferenceWith(method);

        // Force PersonId to be projected regardless of the query
        descriptor.Field(t => t.PersonId).IsProjected(true);

        descriptor.Field("person")
            .Resolve(ctx =>
            {
                var parent = ctx.Parent<RelationshipModel>();
                return new PersonRef { PersonID = parent.PersonId };
            });
    }

    [ReferenceResolver]
    public static async Task<RelationshipModel?> GetByIdAsync(
        int id,
        RelationshipByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(id);
    }
}

[ObjectType("Person")]
[HotChocolate.ApolloFederation.Types.Key("personID")]
public class PersonRef
{
    [HotChocolate.ApolloFederation.Resolvers.ReferenceResolver]
    public static async Task<PersonRef> GetByIdAsync(int personID) 
        => await Task.FromResult(new PersonRef { PersonID = personID });
    
    public int PersonID { get; set; }

    public async Task<RelationshipModel[]> GetRelationships(
        [Parent] PersonRef person,
        RelationshipsByPersonIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(person.PersonID);
    }
}
