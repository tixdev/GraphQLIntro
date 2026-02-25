using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Temporal;

namespace Person.API.Models;

[Table("InternalPerson", Schema = "Person")]
public class InternalPerson : ITemporalEntity
{
    [Key]
    public int InternalPersonID { get; set; }
    public string InternalName { get; set; } = null!;
    public int PltPersonInternalTypeID { get; set; }
    public DateTime ValidStartDate { get; set; }
    public DateTime ValidEndDate { get; set; }
    public int GroupBankID { get; set; }

    // Navigation
    public Person Person { get; set; } = null!;
}
