using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class GenderCodeType : ObjectType<GenderCode>
{
    protected override void Configure(IObjectTypeDescriptor<GenderCode> descriptor)
    {
        var method = typeof(GenderCodeType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("genderCodeID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<GenderCode?> GetByIdAsync(int genderCodeID, GenderCodeByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(genderCodeID);
    }
}
