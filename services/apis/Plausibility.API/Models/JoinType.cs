using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plausibility.API.Models;

[Table("tcd_JoinType", Schema = "plausibility")]
public class JoinType
{
    [Key]
    public int JoinTypeID { get; set; }
    public int LifeCycleStateID { get; set; }
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ICollection<JoinTypeTranslation> Translations { get; set; } = new List<JoinTypeTranslation>();}
