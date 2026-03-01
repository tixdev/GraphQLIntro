using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plausibility.API.Models;

[Table("tcd_PersonCodingType", Schema = "plausibility")]
public class PersonCodingType
{
    [Key]
    public int PersonCodingTypeID { get; set; }
    public int LifeCycleStateID { get; set; }
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ICollection<PersonCodingTypeTranslation> Translations { get; set; } = new List<PersonCodingTypeTranslation>();}
