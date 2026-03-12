using System;
using System.Collections.Generic;

namespace Plausibility.API.Models;

public partial class tcd_AssetStatus
{
    public int AssetStatusID { get; set; }

    public int LifeCycleStateID { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<ttp_AssetStatusLanguage> ttp_AssetStatusLanguages { get; set; } = new List<ttp_AssetStatusLanguage>();
}
