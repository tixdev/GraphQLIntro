using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plausibility.API.Models;

[Table("tcd_MaritalRegime", Schema = "plausibility")]
public class MaritalRegime
{
    [Key]
    public int MaritalRegimeID { get; set; }
    public int LifeCycleStateID { get; set; }
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ICollection<MaritalRegimeTranslation> Translations { get; set; } = new List<MaritalRegimeTranslation>();}
