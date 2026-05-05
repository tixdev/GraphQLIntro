using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Person.API.Models;

[Table("Person", Schema = "Person")]
public class Person
{
    [Key]
    public int PersonID { get; set; }
    public string PersonNumber { get; set; } = null!;
    public int PltPersonNatureID { get; set; }
    public int PltPersonCodingTypeID { get; set; }
    public bool IsMigrated { get; set; }
    public int GroupBankID { get; set; }
    public long? CaseID { get; set; }

    // Navigation properties
    public PersonDetail? PersonDetail { get; set; }
    public NaturalPerson? NaturalPerson { get; set; }
    public LegalPerson? LegalPerson { get; set; }
    public InternalPerson? InternalPerson { get; set; }
    public GroupPerson? GroupPerson { get; set; }
    public PersonOnlineService? PersonOnlineService { get; set; }
    public ICollection<PersonName> PersonName { get; set; } = new List<PersonName>();
    public ICollection<PersonAlternativeCode> PersonAlternativeCode { get; set; } = new List<PersonAlternativeCode>();
}
