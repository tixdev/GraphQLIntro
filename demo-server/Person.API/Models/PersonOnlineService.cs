using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Person.API.Models;

[Table("PersonOnlineService", Schema = "Person")]
public class PersonOnlineService
{
    [Key]
    public int PersonOnlineServiceID { get; set; }
    public int PersonID { get; set; }
    public int GroupBankID { get; set; }
    public bool? PersonDigital { get; set; }
    public DateTime? PersonDigitalDate { get; set; }
    public bool RelationshipDigital { get; set; }
    public DateTime? RelationshipDigitalDate { get; set; }
    public string? CCUser { get; set; }
    public string? CCMS_ID { get; set; }
    public bool Matchability { get; set; }
    public string? Status { get; set; }
    public DateTime ValidStartDate { get; set; }
    public DateTime ValidEndDate { get; set; }
    public long? CaseID { get; set; }
    public string? BankUser { get; set; }

    // Navigation
    public Person Person { get; set; } = null!;
}
