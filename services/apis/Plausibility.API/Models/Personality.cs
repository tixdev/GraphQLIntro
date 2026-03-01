using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plausibility.API.Models;

[Table("tcd_Personality", Schema = "plausibility")]
public class Personality
{
    [Key]
    public int PersonalityID { get; set; }
    public int LifeCycleStateID { get; set; }
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ICollection<PersonalityTranslation> Translations { get; set; } = new List<PersonalityTranslation>();}
