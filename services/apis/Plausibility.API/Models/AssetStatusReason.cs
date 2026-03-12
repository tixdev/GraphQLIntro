using HotChocolate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace Plausibility.API.Models;

[Table("AssetStatusReason", Schema = "plausibility")]
[GraphQLName("PltAssetStatusReason")]
public partial class AssetStatusReason
{
    [Key]
    public int AssetStatusReasonID { get; set; }

    public int LifeCycleStateID { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<AssetStatusReasonTranslation> Translations { get; set; } = new List<AssetStatusReasonTranslation>();
}
