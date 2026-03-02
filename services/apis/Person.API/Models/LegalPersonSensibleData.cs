using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Person.API.Models;

[Table("LegalPersonSensibleData", Schema = "Person")]
public class LegalPersonSensibleData
{
    [Key]
    public int LegalPersonID { get; set; }
    public int BusinessNameID { get; set; }
    public string? BusinessName { get; set; }
    public int? PltRegisteredOfficeID { get; set; }
    public int GroupBankID { get; set; }

    // Navigation
    public LegalPerson? LegalPerson { get; set; }
}
