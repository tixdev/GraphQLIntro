using HotChocolate.Resolvers;
using Person.API.Graph.DataLoaders;
using PersonModel = Person.API.Models.Person;
using Person.API.Models;

namespace Person.API.Graph.Resolvers;

public class PersonResolvers
{
    public async Task<NaturalPerson?> GetNaturalPersonAsync([Parent] PersonModel person, NaturalPersonByPersonIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(person.PersonID);
    }

    public async Task<LegalPerson?> GetLegalPersonAsync([Parent] PersonModel person, LegalPersonByPersonIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(person.PersonID);
    }

    public async Task<InternalPerson?> GetInternalPersonAsync([Parent] PersonModel person, InternalPersonByPersonIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(person.PersonID);
    }

    public async Task<GroupPerson?> GetGroupPersonAsync([Parent] PersonModel person, GroupPersonByPersonIdDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(person.PersonID);
    }

    [HotChocolate.Types.UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 200, IncludeTotalCount = true)]
    public async Task<IEnumerable<PersonName>> GetPersonNameAsync([HotChocolate.Parent] PersonModel person, PersonNameByPersonIdDataLoader dataLoader)
    {
        var results = await dataLoader.LoadAsync(person.PersonID);
        return results ?? Array.Empty<PersonName>();
    }
}
