using HotChocolate;
using HotChocolate.Types;
using Relationship.API.Graph.DataLoaders;
using Relationship.API.Graph.ExternalTypeRefs;
using RelationshipModel = Relationship.API.Models.Relationship;

namespace Relationship.API.Graph.Resolvers;

public class RelationshipResolvers
{
    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 200, IncludeTotalCount = true)]
    public async Task<IEnumerable<PersonRef>> GetPersons(
        [Parent] RelationshipModel relationship,
        PersonsByRelationshipIdDataLoader dataLoader)
    {
        var results = await dataLoader.LoadAsync(relationship.RelationshipID);
        return results ?? Array.Empty<PersonRef>();
    }
}
