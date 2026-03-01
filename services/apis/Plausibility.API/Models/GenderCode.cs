using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plausibility.API.Models;

[Table("tcd_GenderCode", Schema = "plausibility")]
public class GenderCode
{
    [Key]
    public int GenderCodeID { get; set; }
    public int LifeCycleStateID { get; set; }
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ICollection<GenderCodeTranslation> Translations { get; set; } = new List<GenderCodeTranslation>();}
