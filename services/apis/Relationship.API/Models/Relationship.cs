using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Relationship.API.Models;

[Table("Relationship", Schema = "Relationship")]
public class Relationship
{
    [Key]
    public int RelationshipID { get; set; }
    
    public int Number { get; set; }
    
    public int PltRelationshipTypeID { get; set; }
    
    public int GroupBankID { get; set; }

    // Navigation
    public RelationshipName? Name { get; set; }
    public ICollection<RelationshipToPerson> RelationshipToPersons { get; set; } = new List<RelationshipToPerson>();
}
