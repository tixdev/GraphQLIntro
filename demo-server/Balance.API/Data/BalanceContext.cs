using Microsoft.EntityFrameworkCore;
using Balance.API.Models;

namespace Balance.API.Data;

public class BalanceContext : DbContext
{
    public BalanceContext(DbContextOptions<BalanceContext> options) : base(options) { }

    public DbSet<Models.Balance> Balances { get; set; }
}
