using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Person.API.Models;

[Table("PersonDetail", Schema = "Person")]
public class PersonDetail
{
    [Key]
    public int PersonDetailID { get; set; }
    public int PersonID { get; set; }
    public int BasicDataID { get; set; }
    public int? PltPersonAcquisitionSourceID { get; set; }
    public int? PltPersonalityID { get; set; }
    public string? MainOfficer { get; set; }
    public string? DeputyOfficer { get; set; }
    public int GroupBankID { get; set; }
    public long? CaseID { get; set; }

    // Navigation
    public Person Person { get; set; } = null!;
    public PersonDetailSensibleData? SensibleData { get; set; }
}
