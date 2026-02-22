using System.ComponentModel.DataAnnotations;

namespace AssetAPI.Models;

public class Asset
{
    [Key]
    public int Id { get; set; }

    public int RelationshipId { get; set; }

    public required string Name { get; set; } // e.g. Checking Account
    public required string Type { get; set; }
    public required string Number { get; set; }
}
