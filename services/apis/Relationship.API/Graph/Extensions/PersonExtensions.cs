using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Relationship.API.Graph.DataLoaders;
using RelationshipModel = Relationship.API.Models.Relationship;
using HotChocolate.Types;

namespace Relationship.API.Graph.Extensions;

[ObjectType("Person")]
[Key("personID")]
public class PersonExtensions
{
    [ReferenceResolver]
    public static async Task<PersonExtensions> GetByIdAsync(int personID) =>
        await Task.FromResult(new PersonExtensions { PersonID = personID });

    public int PersonID { get; set; }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<RelationshipModel>> GetRelationships(
        [Parent] PersonExtensions person,
        RelationshipsByPersonIdDataLoader dataLoader)
    {
        var results = await dataLoader.LoadAsync(person.PersonID);
        return results ?? Array.Empty<RelationshipModel>();
    }
}
