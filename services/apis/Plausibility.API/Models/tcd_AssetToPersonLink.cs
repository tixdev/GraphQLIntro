using System;
using System.Collections.Generic;

namespace Plausibility.API.Models;

public partial class tcd_AssetToPersonLink
{
    public int AssetToPersonLinkID { get; set; }

    public int LifeCycleStateID { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<ttp_AssetToPersonLinkLanguage> ttp_AssetToPersonLinkLanguages { get; set; } = new List<ttp_AssetToPersonLinkLanguage>();
}
