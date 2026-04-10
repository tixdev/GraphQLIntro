using HotChocolate.ApolloFederation.Resolvers;
using Relationship.API.Graph.DataLoaders;
using RelationshipModel = Relationship.API.Models.Relationship;

namespace Relationship.API.Graph.ExternalTypeRefs;

[ObjectType("Person")]
[HotChocolate.ApolloFederation.Types.Key("personID")]
public class PersonRef
{
    [ReferenceResolver]
    public static async Task<PersonRef> GetByIdAsync(int personID)
        => await Task.FromResult(new PersonRef { PersonID = personID });

    [GraphQLName("personID")]
    public int PersonID { get; set; }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 200, IncludeTotalCount = true)]
    public async Task<IEnumerable<RelationshipModel>> GetRelationships(
        [Parent] PersonRef person,
        RelationshipsByPersonIdDataLoader dataLoader)
    {
        var results = await dataLoader.LoadAsync(person.PersonID);
        return results ?? Array.Empty<RelationshipModel>();
    }
}
