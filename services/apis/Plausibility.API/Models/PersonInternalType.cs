using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plausibility.API.Models;

[Table("tcd_PersonInternalType", Schema = "plausibility")]
public class PersonInternalType
{
    [Key]
    public int PersonInternalTypeID { get; set; }
    public int LifeCycleStateID { get; set; }
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ICollection<PersonInternalTypeTranslation> Translations { get; set; } = new List<PersonInternalTypeTranslation>();}
