using System.Linq;
using Bogus;
using PersonAPI.Models;
using PersonModel = PersonAPI.Models.Person;

namespace PersonAPI.Data;

public static class DataSeeder
{
    public static void Seed(PersonContext context)
    {
        if (context.People.Any()) return;

        var personFaker = new Faker<PersonModel>()
            .RuleFor(p => p.Name, f => f.Name.FullName())
            .RuleFor(p => p.Address, f => f.Address.FullAddress())
            .RuleFor(p => p.Number, f => "0100" + f.Random.Replace("#####"));

        var people = personFaker.Generate(100);
        context.People.AddRange(people);
        context.SaveChanges();
    }
}
