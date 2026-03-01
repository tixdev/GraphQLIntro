using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plausibility.API.Models;

[Table("tcd_RelationshipType", Schema = "plausibility")]
public class RelationshipType
{
    [Key]
    public int RelationshipTypeID { get; set; }
    public int LifeCycleStateID { get; set; }
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ICollection<RelationshipTypeTranslation> Translations { get; set; } = new List<RelationshipTypeTranslation>();}
