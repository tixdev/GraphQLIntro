using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class AssetAlternativeCodeType : ObjectType<AssetAlternativeCode>
{
    protected override void Configure(IObjectTypeDescriptor<AssetAlternativeCode> descriptor)
    {
        descriptor.Name("PltAssetAlternativeCode");
        var method = typeof(AssetAlternativeCodeType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("assetAlternativeCodeID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<AssetAlternativeCode?> GetByIdAsync(int assetAlternativeCodeID, AssetAlternativeCodeByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(assetAlternativeCodeID);
    }
}
