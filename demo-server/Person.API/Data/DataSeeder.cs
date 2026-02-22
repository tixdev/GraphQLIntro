using System.Linq;
using Bogus;
using Person.API.Models;
using PersonModel = Person.API.Models.Person;

namespace Person.API.Data;

public static class DataSeeder
{
    public static void Seed(PersonContext context)
    {
        if (context.Person.Any()) return;

        var personFaker = new Faker<PersonModel>()
            .RuleFor(p => p.Name, f => f.Name.FullName())
            .RuleFor(p => p.Address, f => f.Address.FullAddress())
            .RuleFor(p => p.Number, f => "0100" + f.Random.Replace("#####"));

        var people = personFaker.Generate(100);
        context.Person.AddRange(people);
        context.SaveChanges();
    }
}
