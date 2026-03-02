using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Person.API.Models;

[Table("NaturalPersonSensibleData", Schema = "Person")]
public class NaturalPersonSensibleData
{
    [Key]
    public int NaturalPersonID { get; set; }
    public int LegalSurnameNameID { get; set; }
    public string? LegalSurname { get; set; }
    public string? LegalName { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string? BirthLocation { get; set; }
    public int? PltBirthNationID { get; set; }
    public int? PltSecondNationalityID { get; set; }
    public int? PltResidencyID { get; set; }
    public int GroupBankID { get; set; }

    // Navigation
    public NaturalPerson? NaturalPerson { get; set; }
}
