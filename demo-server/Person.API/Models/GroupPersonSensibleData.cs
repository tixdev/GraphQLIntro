using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Person.API.Models;

[Table("GroupPersonSensibleData", Schema = "Person")]
public class GroupPersonSensibleData
{
    [Key]
    public int GroupPersonID { get; set; }
    public string? Name { get; set; }
    public int GroupBankID { get; set; }

    // Navigation
    public GroupPerson GroupPerson { get; set; } = null!;
}
