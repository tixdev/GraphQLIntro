using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plausibility.API.Models;

[Table("tcd_PersonDependentsNumber", Schema = "plausibility")]
public class PersonDependentsNumber
{
    [Key]
    public int PersonDependentsNumberID { get; set; }
    public int LifeCycleStateID { get; set; }
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ICollection<PersonDependentsNumberTranslation> Translations { get; set; } = new List<PersonDependentsNumberTranslation>();}
