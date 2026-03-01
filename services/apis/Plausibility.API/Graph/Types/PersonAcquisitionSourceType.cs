using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class PersonAcquisitionSourceType : ObjectType<PersonAcquisitionSource>
{
    protected override void Configure(IObjectTypeDescriptor<PersonAcquisitionSource> descriptor)
    {
        var method = typeof(PersonAcquisitionSourceType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("personAcquisitionSourceID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<PersonAcquisitionSource?> GetByIdAsync(int personAcquisitionSourceID, PersonAcquisitionSourceByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(personAcquisitionSourceID);
    }
}
