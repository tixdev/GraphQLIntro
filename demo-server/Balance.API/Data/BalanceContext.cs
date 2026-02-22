using Microsoft.EntityFrameworkCore;
using BalanceAPI.Models;

namespace BalanceAPI.Data;

public class BalanceContext : DbContext
{
    public BalanceContext(DbContextOptions<BalanceContext> options) : base(options) { }

    public DbSet<Balance> Balances { get; set; }
}
