using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PersonAPI.Data;
using PersonAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Services.AddDbContext<PersonContext>(options =>
    options.UseInMemoryDatabase("PersonDomain"));

builder.Services.AddPersonGraphQL();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PersonContext>();
    DataSeeder.Seed(context);
}

app.UseCors();
app.MapGraphQL();

app.Run();
