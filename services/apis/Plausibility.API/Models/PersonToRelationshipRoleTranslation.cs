using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plausibility.API.Models;

[Table("ttp_PersonToRelationshipRoleLanguage", Schema = "plausibility")]
public class PersonToRelationshipRoleTranslation
{
    [Key]
    public int PersonToRelationshipRoleLanguageID { get; set; }
    public int GroupBankID { get; set; }
    public int LanguageID { get; set; }
    public int LifeCycleStateID { get; set; }
    public int PersonToRelationshipRoleID { get; set; }
    public string Code { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public string LongDescription { get; set; } = null!;
    public string NoteDescription { get; set; }
    public DateTime ValidStartDate { get; set; }
    public DateTime? ValidEndDate { get; set; }

}
