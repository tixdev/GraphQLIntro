using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Temporal;

namespace Person.API.Models;

[Table("PersonName", Schema = "Person")]
public class PersonName : ITemporalEntity
{
    [Key]
    public int PersonNameID { get; set; }
    public int PersonID { get; set; }
    public int PltPersonNameTypeID { get; set; }
    public string PersonNameDescription { get; set; } = null!;
    public int GroupBankID { get; set; }
    public long? CaseID { get; set; }

    public DateTime ValidStartDate { get; set; }
    public DateTime ValidEndDate { get; set; }

    // Navigation
    public Person Person { get; set; } = null!;
}
