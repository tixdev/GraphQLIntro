using Microsoft.EntityFrameworkCore;
using RelationshipAPI.Models;

namespace RelationshipAPI.Data;

public class RelationshipContext : DbContext
{
    public RelationshipContext(DbContextOptions<RelationshipContext> options) : base(options) { }

    public DbSet<Relationship> Relationships { get; set; }
}
