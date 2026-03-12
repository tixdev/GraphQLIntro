using System;
using System.Collections.Generic;

namespace Plausibility.API.Models;

public partial class tcd_AssetType
{
    public int AssetTypeID { get; set; }

    public int LifeCycleStateID { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<ttp_AssetTypeLanguage> ttp_AssetTypeLanguages { get; set; } = new List<ttp_AssetTypeLanguage>();
}
