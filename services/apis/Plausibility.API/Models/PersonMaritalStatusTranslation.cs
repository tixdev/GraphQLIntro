using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plausibility.API.Models;

[Table("ttp_PersonMaritalStatusLanguage", Schema = "plausibility")]
public class PersonMaritalStatusTranslation
{
    [Key]
    public int PersonMaritalStatusLanguageID { get; set; }
    public int GroupBankID { get; set; }
    public int LanguageID { get; set; }
    public int PersonMaritalStatusID { get; set; }
    public int LifeCycleStateID { get; set; }
    public string Code { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public string LongDescription { get; set; } = null!;
    public string NoteDescription { get; set; }
    public DateTime ValidStartDate { get; set; }
    public DateTime? ValidEndDate { get; set; }

}
