using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class DebitCardLimitType : ObjectType<DebitCardLimit>
{
    protected override void Configure(IObjectTypeDescriptor<DebitCardLimit> descriptor)
    {
        descriptor.Name("PltDebitCardLimit");
        var method = typeof(DebitCardLimitType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("debitCardLimitID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<DebitCardLimit?> GetByIdAsync(int debitCardLimitID, DebitCardLimitByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(debitCardLimitID);
    }
}
