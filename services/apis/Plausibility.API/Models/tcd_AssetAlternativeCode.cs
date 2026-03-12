using System;
using System.Collections.Generic;

namespace Plausibility.API.Models;

public partial class tcd_AssetAlternativeCode
{
    public int AssetAlternativeCodeID { get; set; }

    public int LifeCycleStateID { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<ttp_AssetAlternativeCodeLanguage> ttp_AssetAlternativeCodeLanguages { get; set; } = new List<ttp_AssetAlternativeCodeLanguage>();
}
