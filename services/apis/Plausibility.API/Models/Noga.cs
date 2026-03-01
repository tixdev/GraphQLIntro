using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plausibility.API.Models;

[Table("tcd_Noga", Schema = "plausibility")]
public class Noga
{
    [Key]
    public int NogaID { get; set; }
    public int LifeCycleStateID { get; set; }
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ICollection<NogaTranslation> Translations { get; set; } = new List<NogaTranslation>();}
