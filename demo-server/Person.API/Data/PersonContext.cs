using Microsoft.EntityFrameworkCore;
using Person.API.Models;

namespace Person.API.Data;

public class PersonContext : DbContext
{
    public PersonContext(DbContextOptions<PersonContext> options) : base(options) { }

    public DbSet<Models.Person> Person { get; set; }
}
