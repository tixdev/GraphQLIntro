using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Person.API.Graph.DataLoaders;
using PersonModel = Person.API.Models.Person;

namespace Person.API.Graph.Types;

public class PersonType : ObjectType<PersonModel>
{
    protected override void Configure(IObjectTypeDescriptor<PersonModel> descriptor)
    {
        var method = typeof(PersonType).GetMethod(nameof(GetPersonByIdAsync))!;

        descriptor.Key("personID").ResolveReferenceWith(method);
    }

    [ReferenceResolver]
    public static async Task<PersonModel?> GetPersonByIdAsync(int id, PersonByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(id);
    }
}
