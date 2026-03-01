using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plausibility.API.Models;

[Table("tcd_PersonToRelationshipRole", Schema = "plausibility")]
public class PersonToRelationshipRole
{
    [Key]
    public int PersonToRelationshipRoleID { get; set; }
    public int LifeCycleStateID { get; set; }
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ICollection<PersonToRelationshipRoleTranslation> Translations { get; set; } = new List<PersonToRelationshipRoleTranslation>();}
