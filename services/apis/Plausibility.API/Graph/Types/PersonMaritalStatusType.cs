using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class PersonMaritalStatusType : ObjectType<PersonMaritalStatus>
{
    protected override void Configure(IObjectTypeDescriptor<PersonMaritalStatus> descriptor)
    {
        var method = typeof(PersonMaritalStatusType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("personMaritalStatusID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<PersonMaritalStatus?> GetByIdAsync(int personMaritalStatusID, PersonMaritalStatusByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(personMaritalStatusID);
    }
}
