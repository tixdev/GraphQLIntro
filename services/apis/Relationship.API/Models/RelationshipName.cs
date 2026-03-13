using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Temporal;

namespace Relationship.API.Models;

[Table("RelationshipName", Schema = "Relationship")]
public class RelationshipName : ITemporalEntity
{
    [Key]
    public int RelationshipID { get; set; }
    
    public string Name { get; set; } = null!;
    
    public int GroupBankID { get; set; }
    
    public DateTime ValidStartDate { get; set; }
    
    public DateTime ValidEndDate { get; set; }

    // Navigation
    public Relationship Relationship { get; set; } = null!;
}
