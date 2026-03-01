using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class PersonCodingTypeType : ObjectType<PersonCodingType>
{
    protected override void Configure(IObjectTypeDescriptor<PersonCodingType> descriptor)
    {
        var method = typeof(PersonCodingTypeType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("personCodingTypeID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<PersonCodingType?> GetByIdAsync(int personCodingTypeID, PersonCodingTypeByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(personCodingTypeID);
    }
}
