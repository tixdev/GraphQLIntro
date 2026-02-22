using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using RelationshipAPI.Data;
using RelationshipAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Services.AddDbContext<RelationshipContext>(options =>
    options.UseInMemoryDatabase("RelationshipDomain"));

builder.Services.AddRelationshipGraphQL();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<RelationshipContext>();
    DataSeeder.Seed(context);
}

app.UseCors();
app.MapGraphQL();

app.Run();
