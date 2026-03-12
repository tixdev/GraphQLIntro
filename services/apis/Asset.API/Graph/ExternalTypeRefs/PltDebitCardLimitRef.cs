using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;

namespace Asset.API.Graph.ExternalTypeRefs;

[ObjectType("PltDebitCardLimit")]
[Key("debitCardLimitID")]
[GraphQLName("PltDebitCardLimit")]
public class PltDebitCardLimitRef
{
    [ReferenceResolver]
    public static async Task<PltDebitCardLimitRef> GetByIdAsync(int debitCardLimitID)
        => await Task.FromResult(new PltDebitCardLimitRef { DebitCardLimitID = debitCardLimitID });

    public int DebitCardLimitID { get; set; }
}
