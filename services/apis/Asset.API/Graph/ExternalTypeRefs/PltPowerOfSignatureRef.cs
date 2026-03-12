using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;

namespace Asset.API.Graph.ExternalTypeRefs;

[ObjectType("PltPowerOfSignature")]
[Key("powerOfSignatureID")]
[GraphQLName("PltPowerOfSignature")]
public class PltPowerOfSignatureRef
{
    [ReferenceResolver]
    public static async Task<PltPowerOfSignatureRef> GetByIdAsync(int powerOfSignatureID)
        => await Task.FromResult(new PltPowerOfSignatureRef { PowerOfSignatureID = powerOfSignatureID });

    public int PowerOfSignatureID { get; set; }
}
