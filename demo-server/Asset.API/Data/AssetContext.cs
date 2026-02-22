using Microsoft.EntityFrameworkCore;
using Asset.API.Models;

namespace Asset.API.Data;

public class AssetContext : DbContext
{
    public AssetContext(DbContextOptions<AssetContext> options) : base(options) { }

    public DbSet<Models.Asset> Assets { get; set; }
}
