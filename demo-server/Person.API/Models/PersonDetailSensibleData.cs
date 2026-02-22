using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Person.API.Models;

[Table("PersonDetailSensibleData", Schema = "Person")]
public class PersonDetailSensibleData
{
    [Key]
    public int PersonDetailID { get; set; }
    public int? PltNationalityID { get; set; }
    public int? PltNogaID { get; set; }
    public int GroupBankID { get; set; }

    // Navigation
    public PersonDetail PersonDetail { get; set; } = null!;
}
