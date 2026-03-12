using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class AssetNoteTypeType : ObjectType<AssetNoteType>
{
    protected override void Configure(IObjectTypeDescriptor<AssetNoteType> descriptor)
    {
        descriptor.Name("PltAssetNoteType");
        var method = typeof(AssetNoteTypeType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("assetNoteTypeID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<AssetNoteType?> GetByIdAsync(int assetNoteTypeID, AssetNoteTypeByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(assetNoteTypeID);
    }
}
