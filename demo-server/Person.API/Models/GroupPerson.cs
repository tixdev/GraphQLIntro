using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Person.API.Models;

[Table("GroupPerson", Schema = "Person")]
public class GroupPerson
{
    [Key]
    public int GroupPersonID { get; set; }
    public string? AgreedName { get; set; }
    public int? GroupNameID { get; set; }
    public int? PltJoinTypeID { get; set; }
    public DateOnly? CreationDate { get; set; }
    public bool HasIndividualJointPersons { get; set; }
    public bool HasOrganizationJointPersons { get; set; }
    public int? LanguageID { get; set; }
    public DateTime ValidStartDate { get; set; }
    public DateTime ValidEndDate { get; set; }
    public int GroupBankID { get; set; }

    // Navigation
    public Person Person { get; set; } = null!;
    public GroupPersonSensibleData? SensibleData { get; set; }
}
