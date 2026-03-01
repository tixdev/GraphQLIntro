using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plausibility.API.Models;

[Table("tcd_PersonOrganizationType", Schema = "plausibility")]
public class PersonOrganizationType
{
    [Key]
    public int PersonOrganizationTypeID { get; set; }
    public int LifeCycleStateID { get; set; }
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ICollection<PersonOrganizationTypeTranslation> Translations { get; set; } = new List<PersonOrganizationTypeTranslation>();}
