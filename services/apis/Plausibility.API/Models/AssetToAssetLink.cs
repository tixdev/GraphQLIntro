using HotChocolate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace Plausibility.API.Models;

[Table("AssetToAssetLink", Schema = "plausibility")]
[GraphQLName("PltAssetToAssetLink")]
public partial class AssetToAssetLink
{
    [Key]
    public int AssetToAssetLinkID { get; set; }

    public int LifeCycleStateID { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<AssetToAssetLinkTranslation> Translations { get; set; } = new List<AssetToAssetLinkTranslation>();
}
