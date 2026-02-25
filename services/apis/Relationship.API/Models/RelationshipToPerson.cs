using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Temporal;

namespace Relationship.API.Models;

[Table("RelationshipToPerson", Schema = "Relationship")]
public class RelationshipToPerson : ITemporalEntity
{
    [Key]
    public int RelationshipToPersonID { get; set; }
    
    public int RelationshipID { get; set; }
    
    public int PersonID { get; set; }
    
    public int PltPersonToRelationshipRoleID { get; set; }
    
    public int GroupBankID { get; set; }
    
    public DateTime ValidStartDate { get; set; }
    
    public DateTime ValidEndDate { get; set; }

    // Navigation
    public Relationship Relationship { get; set; } = null!;
}
