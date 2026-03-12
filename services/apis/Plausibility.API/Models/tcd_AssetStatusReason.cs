using System;
using System.Collections.Generic;

namespace Plausibility.API.Models;

public partial class tcd_AssetStatusReason
{
    public int AssetStatusReasonID { get; set; }

    public int LifeCycleStateID { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<ttp_AssetStatusReasonLanguage> ttp_AssetStatusReasonLanguages { get; set; } = new List<ttp_AssetStatusReasonLanguage>();
}
