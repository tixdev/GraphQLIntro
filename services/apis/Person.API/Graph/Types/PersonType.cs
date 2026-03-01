using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Person.API.Graph.DataLoaders;
using PersonModel = Person.API.Models.Person;

namespace Person.API.Graph.Types;

public class PersonType : ObjectType<PersonModel>
{
    protected override void Configure(IObjectTypeDescriptor<PersonModel> descriptor)
    {
        var method = typeof(PersonType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("personID").ResolveReferenceWith(method);

        descriptor.Field(t => t.PltPersonNatureID).IsProjected();
        descriptor.Field(t => t.PltPersonCodingTypeID).IsProjected();
    }

    [ReferenceResolver]
    public static async Task<PersonModel?> GetByIdAsync(int personID, PersonByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(personID);
    }
}
