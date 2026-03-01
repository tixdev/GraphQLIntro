using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class JoinTypeType : ObjectType<JoinType>
{
    protected override void Configure(IObjectTypeDescriptor<JoinType> descriptor)
    {
        var method = typeof(JoinTypeType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("joinTypeID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<JoinType?> GetByIdAsync(int joinTypeID, JoinTypeByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(joinTypeID);
    }
}
