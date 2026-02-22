using System.ComponentModel.DataAnnotations;

namespace RelationshipAPI.Models;

public class Relationship
{
    [Key]
    public int Id { get; set; }
    
    public int PersonId { get; set; }

    public int Number { get; set; } // 6 digits between 200000 and 800000
    public required string Type { get; set; } // e.g. Parent, Partner
}
