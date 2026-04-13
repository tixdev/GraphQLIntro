using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;
using Person.API.Graph.DataLoaders;
using Person.API.Graph.Resolvers;
using PersonModel = Person.API.Models.Person;

namespace Person.API.Graph.Types;

public class PersonType : ObjectType<PersonModel>
{
    protected override void Configure(IObjectTypeDescriptor<PersonModel> descriptor)
    {
        var method = typeof(PersonType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("personID").ResolveReferenceWith(method);

        descriptor.Field(t => t.PersonID).Name("personID").IsProjected(true);
        descriptor.Field(t => t.PltPersonNatureID).IsProjected();
        descriptor.Field(t => t.PltPersonCodingTypeID).IsProjected();        
        descriptor.Field(t => t.NaturalPerson)
            .ResolveWith<PersonResolvers>(r => r.GetNaturalPersonAsync(default!, default!))
            .IsProjected(false);
            
        descriptor.Field(t => t.LegalPerson)
            .ResolveWith<PersonResolvers>(r => r.GetLegalPersonAsync(default!, default!))
            .IsProjected(false);
            
        descriptor.Field(t => t.InternalPerson)
            .ResolveWith<PersonResolvers>(r => r.GetInternalPersonAsync(default!, default!))
            .IsProjected(false);
            
        descriptor.Field(t => t.GroupPerson)
            .ResolveWith<PersonResolvers>(r => r.GetGroupPersonAsync(default!, default!))
            .IsProjected(false);

        descriptor.Field(t => t.PersonDetail)
            .ResolveWith<PersonResolvers>(r => r.GetPersonDetailAsync(default!, default!))
            .IsProjected(false);
            
        descriptor.Field(t => t.PersonName)
            .ResolveWith<PersonResolvers>(r => r.GetPersonNameAsync(default!, default!))
            .IsProjected(false);
    }

    [ReferenceResolver]
    public static async Task<PersonModel?> GetByIdAsync(int personID, PersonByIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(personID);
    }
}
