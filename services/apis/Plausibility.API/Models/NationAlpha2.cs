using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plausibility.API.Models;

[Table("tcd_NationAlpha2", Schema = "plausibility")]
public class NationAlpha2
{
    [Key]
    public int NationAlpha2ID { get; set; }
    public int LifeCycleStateID { get; set; }
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ICollection<NationAlpha2Translation> Translations { get; set; } = new List<NationAlpha2Translation>();}
