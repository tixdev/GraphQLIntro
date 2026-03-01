using HotChocolate;
using HotChocolate.Types;
using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Asset.API.Graph.DataLoaders;
using AssetModel = Asset.API.Models.Asset;

namespace Asset.API.Graph.ExternalTypeRefs;

[ObjectType("Relationship")]
[Key("relationshipID")]
public class RelationshipRef
{
    [ReferenceResolver]
    public static async Task<RelationshipRef> GetByIdAsync(int relationshipID)
        => await Task.FromResult(new RelationshipRef { RelationshipID = relationshipID });

    public int RelationshipID { get; set; }

    public async Task<AssetModel[]> GetAssets(
        [Parent] RelationshipRef relationship,
        AssetsByRelationshipIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(relationship.RelationshipID);
    }
}
