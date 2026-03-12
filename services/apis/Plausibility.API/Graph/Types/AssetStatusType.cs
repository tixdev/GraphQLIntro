using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class AssetStatusType : ObjectType<AssetStatus>
{
    protected override void Configure(IObjectTypeDescriptor<AssetStatus> descriptor)
    {
        descriptor.Name("PltAssetStatus");
        var method = typeof(AssetStatusType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("assetStatusID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<AssetStatus?> GetByIdAsync(int assetStatusID, AssetStatusByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(assetStatusID);
    }
}
