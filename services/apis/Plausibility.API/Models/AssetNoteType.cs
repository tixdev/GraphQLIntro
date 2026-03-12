using HotChocolate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace Plausibility.API.Models;

[Table("AssetNoteType", Schema = "plausibility")]
[GraphQLName("PltAssetNoteType")]
public partial class AssetNoteType
{
    [Key]
    public int AssetNoteTypeID { get; set; }

    public int LifeCycleStateID { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<AssetNoteTypeTranslation> Translations { get; set; } = new List<AssetNoteTypeTranslation>();
}
