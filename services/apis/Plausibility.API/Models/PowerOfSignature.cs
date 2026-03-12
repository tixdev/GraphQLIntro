using HotChocolate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace Plausibility.API.Models;

[Table("PowerOfSignature", Schema = "plausibility")]
[GraphQLName("PltPowerOfSignature")]
public partial class PowerOfSignature
{
    [Key]
    public int PowerOfSignatureID { get; set; }

    public int LifeCycleStateID { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<PowerOfSignatureTranslation> Translations { get; set; } = new List<PowerOfSignatureTranslation>();
}
