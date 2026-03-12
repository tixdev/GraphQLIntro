using System;
using System.Collections.Generic;

namespace Plausibility.API.Models;

public partial class tcd_AssetToAssetLink
{
    public int AssetToAssetLinkID { get; set; }

    public int LifeCycleStateID { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<ttp_AssetToAssetLinkLanguage> ttp_AssetToAssetLinkLanguages { get; set; } = new List<ttp_AssetToAssetLinkLanguage>();
}
