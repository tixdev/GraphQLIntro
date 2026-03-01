using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plausibility.API.Models;

[Table("tcd_Pep", Schema = "plausibility")]
public class Pep
{
    [Key]
    public int PepID { get; set; }
    public int LifeCycleStateID { get; set; }
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ICollection<PepTranslation> Translations { get; set; } = new List<PepTranslation>();}
