using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class AssetStatusReasonType : ObjectType<AssetStatusReason>
{
    protected override void Configure(IObjectTypeDescriptor<AssetStatusReason> descriptor)
    {
        descriptor.Name("PltAssetStatusReason");
        var method = typeof(AssetStatusReasonType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("assetStatusReasonID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<AssetStatusReason?> GetByIdAsync(int assetStatusReasonID, AssetStatusReasonByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(assetStatusReasonID);
    }
}
