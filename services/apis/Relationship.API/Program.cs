using Microsoft.EntityFrameworkCore;
using Relationship.API.Data;
using Relationship.API.Extensions;
using Shared.Temporal;
using Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITemporalContext, TemporalContext>();

builder.Services.AddSingleton<TestMetrics>();
builder.Services.AddApiServiceOpenTelemetry("Relationship.API");

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Services.AddDbContext<RelationshipContext>((sp, options) =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .AddInterceptors(new MetricsDbCommandInterceptor(sp.GetRequiredService<TestMetrics>())),
    ServiceLifetime.Transient);

builder.Services.AddRelationshipGraphQL();

var app = builder.Build();

app.UseCors();
app.Use(async (ctx, next) => {
    if (ctx.Request.Path.StartsWithSegments("/graphql"))
        ctx.RequestServices.GetRequiredService<TestMetrics>().GraphQLRequests++;
    await next();
});

app.MapGet("/_metrics", (TestMetrics m) => Results.Ok(new {
    m.GraphQLRequests, m.SqlQueries,
    SqlStatements = m.SqlStatements.ToList()
}));
app.MapDelete("/_metrics", (TestMetrics m) => { m.Reset(); return Results.Ok(); });

app.MapGraphQL();

app.Run();
