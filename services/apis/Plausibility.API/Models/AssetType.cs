using HotChocolate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace Plausibility.API.Models;

[Table("tcd_AssetType", Schema = "plausibility")]
[GraphQLName("PltAssetType")]
public partial class AssetType
{
    [Key]
    public int AssetTypeID { get; set; }

    public int LifeCycleStateID { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<AssetTypeTranslation> Translations { get; set; } = new List<AssetTypeTranslation>();
}
