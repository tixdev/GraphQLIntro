using System;
using System.Collections.Generic;

namespace Plausibility.API.Models;

public partial class tcd_AssetToRelationshipLink
{
    public int AssetToRelationshipLinkID { get; set; }

    public int LifeCycleStateID { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<ttp_AssetToRelationshipLinkLanguage> ttp_AssetToRelationshipLinkLanguages { get; set; } = new List<ttp_AssetToRelationshipLinkLanguage>();
}
