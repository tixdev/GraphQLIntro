using System;
using System.Collections.Generic;

namespace Plausibility.API.Models;

public partial class tcd_PowerOfSignature
{
    public int PowerOfSignatureID { get; set; }

    public int LifeCycleStateID { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<ttp_PowerOfSignatureLanguage> ttp_PowerOfSignatureLanguages { get; set; } = new List<ttp_PowerOfSignatureLanguage>();
}
