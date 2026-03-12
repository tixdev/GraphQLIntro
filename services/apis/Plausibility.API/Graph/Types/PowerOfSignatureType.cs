using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class PowerOfSignatureType : ObjectType<PowerOfSignature>
{
    protected override void Configure(IObjectTypeDescriptor<PowerOfSignature> descriptor)
    {
        descriptor.Name("PltPowerOfSignature");
        var method = typeof(PowerOfSignatureType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("powerOfSignatureID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<PowerOfSignature?> GetByIdAsync(int powerOfSignatureID, PowerOfSignatureByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(powerOfSignatureID);
    }
}
