using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Asset.API.Data;
using Asset.API.Extensions;
using Shared.Temporal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITemporalContext, TemporalContext>();

builder.Services.AddSingleton<TestMetrics>();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Services.AddDbContext<AssetContext>((sp, options) =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .AddInterceptors(new MetricsDbCommandInterceptor(sp.GetRequiredService<TestMetrics>())));

builder.Services.AddAssetGraphQL();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AssetContext>();
    context.Database.EnsureCreated();
    DataSeeder.Seed(context);
}

app.UseCors();
app.Use(async (ctx, next) =>
{
    if (ctx.Request.Path.StartsWithSegments("/graphql"))
        ctx.RequestServices.GetRequiredService<TestMetrics>().GraphQLRequests++;
    await next();
});

app.MapGet("/_metrics", (TestMetrics m) => Results.Ok(new
{
    m.GraphQLRequests,
    m.SqlQueries,
    SqlStatements = m.SqlStatements.ToList()
}));
app.MapDelete("/_metrics", (TestMetrics m) => { m.Reset(); return Results.Ok(); });

app.MapGraphQL();

app.Run();
