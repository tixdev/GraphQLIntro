using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bogus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

// Configure EF Core In-Memory
builder.Services.AddDbContext<PersonContext>(options =>
    options.UseInMemoryDatabase("BankingDomain"));

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

// Seed Data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PersonContext>();
    DataSeeder.Seed(context);
}

app.UseCors();
app.MapGraphQL();

app.Run();

public class Query
{
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Person> GetPerson([Service] PersonContext context) => context.People.AsNoTracking();

    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Relationship> GetRelationship([Service] PersonContext context) => context.Relationships.AsNoTracking();
}

public class Person
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string Number { get; set; } // Starts with 0100 e.g. 010000332

    [UseFiltering]
    [UseSorting]
    [GraphQLName("relationship")]
    public ICollection<Relationship> Relationships { get; set; } = new List<Relationship>();
}

public class Relationship
{
    [Key]
    public int Id { get; set; }
    
    public int PersonId { get; set; }
    [ForeignKey("PersonId")]
    public Person? Person { get; set; }

    public int Number { get; set; } // 6 digits between 200000 and 800000
    public required string Type { get; set; } // e.g. Parent, Partner

    [UseFiltering]
    [UseSorting]
    [GraphQLName("asset")]
    public ICollection<Asset> Assets { get; set; } = new List<Asset>();
}

public class Asset
{
    [Key]
    public int Id { get; set; }

    public int RelationshipId { get; set; }
    [ForeignKey("RelationshipId")]
    public Relationship? Relationship { get; set; }

    public required string Name { get; set; } // e.g. Checking Account
    public required string Type { get; set; }
    public required string Number { get; set; }

    [UseFiltering]
    [UseSorting]
    [GraphQLName("balance")]
    public ICollection<Balance> Balances { get; set; } = new List<Balance>();
}

public class Balance
{
    [Key]
    public int Id { get; set; }

    public int AssetId { get; set; }
    [ForeignKey("AssetId")]
    public Asset? Asset { get; set; }

    public decimal Amount { get; set; }
    public required string Currency { get; set; }
}

public class PersonContext : DbContext
{
    public PersonContext(DbContextOptions<PersonContext> options) : base(options) { }

    public DbSet<Person> People { get; set; }
    public DbSet<Relationship> Relationships { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Balance> Balances { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasMany(p => p.Relationships)
            .WithOne(r => r.Person)
            .HasForeignKey(r => r.PersonId);

        modelBuilder.Entity<Relationship>()
            .HasMany(r => r.Assets)
            .WithOne(a => a.Relationship)
            .HasForeignKey(a => a.RelationshipId);

        modelBuilder.Entity<Asset>()
            .HasMany(a => a.Balances)
            .WithOne(b => b.Asset)
            .HasForeignKey(b => b.AssetId);
    }
}

public static class DataSeeder
{
    public static void Seed(PersonContext context)
    {
        if (context.People.Any()) return;

        var faker = new Faker(); // Global faker for random common things

        // 1. Generate People
        var personFaker = new Faker<Person>()
            .RuleFor(p => p.Name, f => f.Name.FullName())
            .RuleFor(p => p.Address, f => f.Address.FullAddress())
            .RuleFor(p => p.Number, f => "0100" + f.Random.Replace("#####")); // 0100 + 5 random digits

        var people = personFaker.Generate(100);
        context.People.AddRange(people);
        context.SaveChanges();

        // 2. Generate Relationships (1-2 per person)
        var relationships = new List<Relationship>();
        var relationshipIdCounter = 1;
        var relationshipFaker = new Faker<Relationship>()
            .RuleFor(r => r.Type, f => f.PickRandom("Parent", "Partner", "Child", "Sibling", "Colleague"))
            .RuleFor(r => r.Number, f => f.Random.Int(200000, 800000));

        foreach (var person in people)
        {
            var count = faker.Random.Int(1, 2);
            for (int i = 0; i < count; i++)
            {
                var rel = relationshipFaker.Generate();
                rel.PersonId = person.Id;
                // rel.Id will be auto-generated by DB but standard Faker might try to set it if not careful. 
                // Since we rely on EF Core InMemory auto-increment, we just add to context.
                // However, doing it in bulk is faster.
                relationships.Add(rel);
            }
        }
        context.Relationships.AddRange(relationships);
        context.SaveChanges();

        // 3. Generate Assets (1-3 per relationship)
        var assets = new List<Asset>();
        var assetFaker = new Faker<Asset>()
            .RuleFor(a => a.Name, f => f.Commerce.ProductName())
            .RuleFor(a => a.Type, f => f.PickRandom("Account", "Portfolio", "RealEstate", "Vehicle"))
            .RuleFor(a => a.Number, f => f.Finance.Account());

        foreach (var rel in relationships)
        {
            var count = faker.Random.Int(1, 3);
            for (int i = 0; i < count; i++)
            {
                var asset = assetFaker.Generate();
                asset.RelationshipId = rel.Id;
                assets.Add(asset);
            }
        }
        context.Assets.AddRange(assets);
        context.SaveChanges();

        // 4. Generate Balances (1 per asset usually, maybe more for history? keeping it simple: 1)
        var balances = new List<Balance>();
        var balanceFaker = new Faker<Balance>()
            .RuleFor(b => b.Amount, f => f.Finance.Amount(-1000, 100000))
            .RuleFor(b => b.Currency, f => f.Finance.Currency().Code);

        foreach (var asset in assets)
        {
            var balance = balanceFaker.Generate();
            balance.AssetId = asset.Id;
            balances.Add(balance);
        }
        context.Balances.AddRange(balances);
        context.SaveChanges();
    }
}
