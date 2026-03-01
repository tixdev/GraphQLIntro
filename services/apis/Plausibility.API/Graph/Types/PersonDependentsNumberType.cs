using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Plausibility.API.Graph.DataLoaders;
using Plausibility.API.Models;

namespace Plausibility.API.Graph.Types;

public class PersonDependentsNumberType : ObjectType<PersonDependentsNumber>
{
    protected override void Configure(IObjectTypeDescriptor<PersonDependentsNumber> descriptor)
    {
        var method = typeof(PersonDependentsNumberType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("personDependentsNumberID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<PersonDependentsNumber?> GetByIdAsync(int personDependentsNumberID, PersonDependentsNumberByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(personDependentsNumberID);
    }
}
