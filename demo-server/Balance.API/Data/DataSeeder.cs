using System.Collections.Generic;
using System.Linq;
using Bogus;
using BalanceAPI.Models;
using BalanceModel = BalanceAPI.Models.Balance;

namespace BalanceAPI.Data;

public static class DataSeeder
{
    public static void Seed(BalanceContext context)
    {
        if (context.Balances.Any()) return;

        var faker = new Faker();
        var balances = new List<BalanceModel>();
        var balanceFaker = new Faker<BalanceModel>()
            .RuleFor(b => b.Amount, f => f.Finance.Amount(-1000, 100000))
            .RuleFor(b => b.Currency, f => f.Finance.Currency().Code);

        // Generate balances for Asset IDs 1 to 300 (since 150 rels get 1-3 assets each, let's assume ~300 assets)
        for (int assetId = 1; assetId <= 300; assetId++)
        {
            var balance = balanceFaker.Generate();
            balance.AssetId = assetId;
            balances.Add(balance);
        }
        
        context.Balances.AddRange(balances);
        context.SaveChanges();
    }
}
