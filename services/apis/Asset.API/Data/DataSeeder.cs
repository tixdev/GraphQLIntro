using System.Collections.Generic;
using System.Linq;
using Bogus;
using Asset.API.Models;
using AssetModel = Asset.API.Models.Asset;

namespace Asset.API.Data;

public static class DataSeeder
{
    public static void Seed(AssetContext context)
    {
        if (context.Assets.Any()) return;

        var faker = new Faker();
        var assets = new List<AssetModel>();
        var assetFaker = new Faker<AssetModel>()
            .RuleFor(a => a.Name, f => f.Commerce.ProductName())
            .RuleFor(a => a.Type, f => f.PickRandom("Account", "Portfolio", "RealEstate", "Vehicle"))
            .RuleFor(a => a.Number, f => f.Finance.Account());

        // Generate assets for Relationship IDs 1 to 150 (since person 1-100 get 1-2 relationships each)
        for (int relId = 1; relId <= 150; relId++)
        {
            var count = faker.Random.Int(1, 3);
            for (int i = 0; i < count; i++)
            {
                var asset = assetFaker.Generate();
                asset.RelationshipId = relId;
                assets.Add(asset);
            }
        }

        context.Assets.AddRange(assets);
        context.SaveChanges();
    }
}
