using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plausibility.API.Models;

[Table("tcd_PersonNature", Schema = "plausibility")]
public class PersonNature
{
    [Key]
    public int PersonNatureID { get; set; }
    public int LifeCycleStateID { get; set; }
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ICollection<PersonNatureTranslation> Translations { get; set; } = new List<PersonNatureTranslation>();}
