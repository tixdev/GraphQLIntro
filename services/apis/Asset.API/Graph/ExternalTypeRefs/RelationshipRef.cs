using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Asset.API.Graph.DataLoaders;
using AssetModel = Asset.API.Models.Asset;

namespace Asset.API.Graph.ExternalTypeRefs;

[ObjectType("Relationship")]
[Key("relationshipID")]
[GraphQLName("Relationship")]
public class RelationshipRef
{
    [ReferenceResolver]
    public static async Task<RelationshipRef> GetByIdAsync(int relationshipID)
        => await Task.FromResult(new RelationshipRef { RelationshipID = relationshipID });

    public int RelationshipID { get; set; }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<IEnumerable<AssetModel>> GetAssets(
        [Parent] RelationshipRef relationship,
        AssetsByRelationshipIdDataLoader dataLoader)
    {
        var results = await dataLoader.LoadAsync(relationship.RelationshipID);
        return results ?? Array.Empty<AssetModel>();
    }
}
