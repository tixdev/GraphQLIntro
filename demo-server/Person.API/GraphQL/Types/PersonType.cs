using HotChocolate.Types;
using HotChocolate.ApolloFederation.Types;
using PersonAPI.Data;
using PersonModel = PersonAPI.Models.Person;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace PersonAPI.GraphQL.Types;

public class PersonType : ObjectType<PersonModel>
{
    protected override void Configure(IObjectTypeDescriptor<PersonModel> descriptor)
    {
        var method = typeof(PersonType).GetMethod(nameof(GetPersonByIdAsync))!;
        descriptor.Key("id").ResolveReferenceWith(method);
    }

    public static async Task<PersonModel?> GetPersonByIdAsync(
        int id,
        [Service] PersonContext ctx)
    {
        return await ctx.Person.FirstOrDefaultAsync(p => p.Id == id);
    }
}
