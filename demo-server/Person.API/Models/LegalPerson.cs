using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Person.API.Models;

[Table("LegalPerson", Schema = "Person")]
public class LegalPerson
{
    [Key]
    public int LegalPersonID { get; set; }
    public string? AgreedName { get; set; }
    public DateOnly? FoundationDate { get; set; }
    public int? LanguageID { get; set; }
    public int? PltEmployeesRangeNumberID { get; set; }
    public int? PltPersonOrganizationTypeID { get; set; }
    public bool? EconomicallyExposedLegal { get; set; }
    public DateTime ValidStartDate { get; set; }
    public DateTime ValidEndDate { get; set; }
    public int GroupBankID { get; set; }
    public string? DomiciliaryCompanyPurpose { get; set; }
    public string? ComplexStructurePurpose { get; set; }

    // Navigation
    public Person Person { get; set; } = null!;
    public LegalPersonSensibleData? SensibleData { get; set; }
}
