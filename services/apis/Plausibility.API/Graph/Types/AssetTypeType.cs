using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class AssetTypeType : ObjectType<AssetType>
{
    protected override void Configure(IObjectTypeDescriptor<AssetType> descriptor)
    {
        descriptor.Name("PltAssetType");
        var method = typeof(AssetTypeType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("assetTypeID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<AssetType?> GetByIdAsync(int assetTypeID, AssetTypeByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(assetTypeID);
    }
}
