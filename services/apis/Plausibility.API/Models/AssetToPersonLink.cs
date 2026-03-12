using HotChocolate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace Plausibility.API.Models;

[Table("AssetToPersonLink", Schema = "plausibility")]
[GraphQLName("PltAssetToPersonLink")]
public partial class AssetToPersonLink
{
    [Key]
    public int AssetToPersonLinkID { get; set; }

    public int LifeCycleStateID { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<AssetToPersonLinkTranslation> Translations { get; set; } = new List<AssetToPersonLinkTranslation>();
}
