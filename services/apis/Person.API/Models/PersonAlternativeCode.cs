using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Temporal;

namespace Person.API.Models;

[Table("PersonAlternativeCode", Schema = "Person")]
public class PersonAlternativeCode : ITemporalEntity
{
    [Key]
    public int PersonAlternativeCodeID { get; set; }
    public int PersonID { get; set; }
    public int PltPersonAlternativeCodeTypeID { get; set; }
    public string AlternativeCodeDescription { get; set; } = null!;
    public DateTime ValidStartDate { get; set; }
    public DateTime ValidEndDate { get; set; }
    public int GroupBankID { get; set; }
    public long? CaseID { get; set; }

    // Navigation
    public Person Person { get; set; } = null!;
}
