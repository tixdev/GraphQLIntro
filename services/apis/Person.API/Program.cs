using Microsoft.EntityFrameworkCore;
using Person.API.Data;
using Person.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<TestMetrics>();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Services.AddDbContext<PersonContext>((sp, options) =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .AddInterceptors(new MetricsDbCommandInterceptor(sp.GetRequiredService<TestMetrics>())));

builder.Services.AddPersonGraphQL();

var app = builder.Build();

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
