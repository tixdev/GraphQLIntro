using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class BusinessDateTypeType : ObjectType<BusinessDateType>
{
    protected override void Configure(IObjectTypeDescriptor<BusinessDateType> descriptor)
    {
        descriptor.Name("PltBusinessDateType");
        var method = typeof(BusinessDateTypeType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("businessDateTypeID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<BusinessDateType?> GetByIdAsync(int businessDateTypeID, BusinessDateTypeByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(businessDateTypeID);
    }
}
