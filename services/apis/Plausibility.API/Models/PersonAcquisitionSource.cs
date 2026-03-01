using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plausibility.API.Models;

[Table("tcd_PersonAcquisitionSource", Schema = "plausibility")]
public class PersonAcquisitionSource
{
    [Key]
    public int PersonAcquisitionSourceID { get; set; }
    public int LifeCycleStateID { get; set; }
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ICollection<PersonAcquisitionSourceTranslation> Translations { get; set; } = new List<PersonAcquisitionSourceTranslation>();}
