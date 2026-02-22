using Microsoft.EntityFrameworkCore;
using AssetAPI.Models;

namespace AssetAPI.Data;

public class AssetContext : DbContext
{
    public AssetContext(DbContextOptions<AssetContext> options) : base(options) { }

    public DbSet<Asset> Assets { get; set; }
}
