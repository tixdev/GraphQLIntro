using HotChocolate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace Plausibility.API.Models;

[Table("BusinessDateType", Schema = "plausibility")]
[GraphQLName("PltBusinessDateType")]
public partial class BusinessDateType
{
    [Key]
    public int BusinessDateTypeID { get; set; }

    public int LifeCycleStateID { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<BusinessDateTypeTranslation> Translations { get; set; } = new List<BusinessDateTypeTranslation>();
}
