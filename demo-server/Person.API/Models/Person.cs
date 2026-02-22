using System.ComponentModel.DataAnnotations;

namespace Person.API.Models;

public class Person
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string Number { get; set; }
}
