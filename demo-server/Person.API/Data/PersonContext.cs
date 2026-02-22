using Microsoft.EntityFrameworkCore;
using PersonAPI.Models;

namespace PersonAPI.Data;

public class PersonContext : DbContext
{
    public PersonContext(DbContextOptions<PersonContext> options) : base(options) { }

    public DbSet<Person> People { get; set; }
}
