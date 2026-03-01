using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plausibility.API.Models;

[Table("tcd_PersonMaritalStatus", Schema = "plausibility")]
public class PersonMaritalStatus
{
    [Key]
    public int PersonMaritalStatusID { get; set; }
    public int LifeCycleStateID { get; set; }
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ICollection<PersonMaritalStatusTranslation> Translations { get; set; } = new List<PersonMaritalStatusTranslation>();}
