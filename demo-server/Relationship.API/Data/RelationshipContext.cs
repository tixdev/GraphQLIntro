using Microsoft.EntityFrameworkCore;
using Relationship.API.Models;

namespace Relationship.API.Data;

public class RelationshipContext : DbContext
{
    public RelationshipContext(DbContextOptions<RelationshipContext> options) : base(options) { }

    public DbSet<Models.Relationship> Relationships { get; set; }
}
