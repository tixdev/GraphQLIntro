using Microsoft.EntityFrameworkCore;
using Product.API.Data;
using Product.API.Extensions;
using Shared.Temporal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITemporalContext, TemporalContext>();
builder.Services.AddSingleton<TestMetrics>();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Server=localhost,1433;Database=Dynacos;User Id=sa;Password=Pass@word;TrustServerCertificate=True;";

builder.Services.AddDbContext<ProductDbContext>((sp, options) =>
    options.UseSqlServer(connectionString)
           .AddInterceptors(new MetricsDbCommandInterceptor(sp.GetRequiredService<TestMetrics>())), 
    ServiceLifetime.Transient);

builder.Services.AddGraphQLServices(builder.Configuration);

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
