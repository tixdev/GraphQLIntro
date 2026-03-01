using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Plausibility.API.Data;
using Plausibility.API.Graph.Queries;
using HotChocolate.Types;
using HotChocolate.ApolloFederation.Types;
using Plausibility.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add Database Context
var connectionString = builder.Configuration.GetConnectionString("Dynacos");
builder.Services.AddDbContextPool<PlausibilityDbContext>(options =>
   options.UseSqlServer(connectionString));

// Add DI Services
builder.Services.AddScoped<Shared.Temporal.ITemporalContext, Shared.Temporal.TemporalContext>();

// Add GraphQL Services
builder.Services.AddPlausibilityGraphQL();

var app = builder.Build();

app.UseRouting();

app.MapGraphQL();

app.Run();
