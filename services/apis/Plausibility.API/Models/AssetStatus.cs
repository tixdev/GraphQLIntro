using HotChocolate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace Plausibility.API.Models;

[Table("AssetStatus", Schema = "plausibility")]
[GraphQLName("PltAssetStatus")]
public partial class AssetStatus
{
    [Key]
    public int AssetStatusID { get; set; }

    public int LifeCycleStateID { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<AssetStatusTranslation> Translations { get; set; } = new List<AssetStatusTranslation>();
}
