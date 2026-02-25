using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Temporal;

namespace Person.API.Models;

[Table("NaturalPerson", Schema = "Person")]
public class NaturalPerson : ITemporalEntity
{
    [Key]
    public int NaturalPersonID { get; set; }
    public string? AgreedName { get; set; }
    public int? PltGenderCodeID { get; set; }
    public int? FamilySituationID { get; set; }
    public int? PltPersonMaritalStatusID { get; set; }
    public int? PltMaritalRegimeID { get; set; }
    public int? PltPersonDependentsNumberID { get; set; }
    public int? PltPersonProfessionID { get; set; }
    public int? LanguageID { get; set; }
    public bool PoliticallyExposed { get; set; }
    public int? PltPepID { get; set; }
    public bool? EconomicallyExposed { get; set; }
    public bool BorderWorker { get; set; }
    public int PINID { get; set; }
    public int? PIN { get; set; }
    public DateTime ValidStartDate { get; set; }
    public DateTime ValidEndDate { get; set; }
    public int GroupBankID { get; set; }
    public bool? HasRetirementFund { get; set; }
    public bool? WorkOverRetirement { get; set; }

    // Navigation
    public Person Person { get; set; } = null!;
    public NaturalPersonSensibleData? SensibleData { get; set; }
}
