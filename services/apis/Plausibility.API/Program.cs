using Microsoft.EntityFrameworkCore;
using Plausibility.API.Data;
using Plausibility.API.Extensions;
using Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Dynacos");
builder.Services.AddDbContext<PlausibilityDbContext>(options =>
   options.UseSqlServer(connectionString),
   ServiceLifetime.Transient);

builder.Services.AddApiServiceOpenTelemetry("Plausibility.API");

builder.Services.AddScoped<Shared.Temporal.ITemporalContext, Shared.Temporal.TemporalContext>();

builder.Services.AddPlausibilityGraphQL();

var app = builder.Build();

app.UseRouting();

app.MapGraphQL();

app.Run();
