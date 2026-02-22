using System.Collections.Generic;
using System.Linq;
using Bogus;
using Relationship.API.Models;
using RelationshipModel = Relationship.API.Models.Relationship;

namespace Relationship.API.Data;

public static class DataSeeder
{
    public static void Seed(RelationshipContext context)
    {
        if (context.Relationships.Any()) return;

        var faker = new Faker();
        var relationships = new List<RelationshipModel>();
        var relationshipFaker = new Faker<RelationshipModel>()
            .RuleFor(r => r.Type, f => f.PickRandom("Parent", "Partner", "Child", "Sibling", "Colleague"))
            .RuleFor(r => r.Number, f => f.Random.Int(200000, 800000));

        // Generate relationships for Person IDs 1 to 100
        for (int personId = 1; personId <= 100; personId++)
        {
            var count = faker.Random.Int(1, 2);
            for (int i = 0; i < count; i++)
            {
                var rel = relationshipFaker.Generate();
                rel.PersonId = personId;
                relationships.Add(rel);
            }
        }
        
        context.Relationships.AddRange(relationships);
        context.SaveChanges();
    }
}
