using HotChocolate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace Plausibility.API.Models;

[Table("AssetToRelationshipLink", Schema = "plausibility")]
[GraphQLName("PltAssetToRelationshipLink")]
public partial class AssetToRelationshipLink
{
    [Key]
    public int AssetToRelationshipLinkID { get; set; }

    public int LifeCycleStateID { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<AssetToRelationshipLinkTranslation> Translations { get; set; } = new List<AssetToRelationshipLinkTranslation>();
}
