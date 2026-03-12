using HotChocolate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace Plausibility.API.Models;

[Table("AssetAlternativeCode", Schema = "plausibility")]
[GraphQLName("PltAssetAlternativeCode")]
public partial class AssetAlternativeCode
{
    [Key]
    public int AssetAlternativeCodeID { get; set; }

    public int LifeCycleStateID { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<AssetAlternativeCodeTranslation> Translations { get; set; } = new List<AssetAlternativeCodeTranslation>();
}
