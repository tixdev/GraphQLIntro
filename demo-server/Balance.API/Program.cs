using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BalanceAPI.Data;
using BalanceAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Services.AddDbContext<BalanceContext>(options =>
    options.UseInMemoryDatabase("BalanceDomain"));

builder.Services.AddBalanceGraphQL();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BalanceContext>();
    DataSeeder.Seed(context);
}

app.UseCors();
app.MapGraphQL();

app.Run();
