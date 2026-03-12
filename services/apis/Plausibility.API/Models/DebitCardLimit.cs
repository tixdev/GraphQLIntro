using HotChocolate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace Plausibility.API.Models;

[Table("DebitCardLimit", Schema = "plausibility")]
[GraphQLName("PltDebitCardLimit")]
public partial class DebitCardLimit
{
    [Key]
    public int DebitCardLimitID { get; set; }

    public int LifeCycleStateID { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<DebitCardLimitTranslation> Translations { get; set; } = new List<DebitCardLimitTranslation>();
}
