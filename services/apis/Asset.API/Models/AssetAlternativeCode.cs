using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class AssetAlternativeCode
{
    public int AssetAlternativeCodeID { get; set; }

    public int AssetID { get; set; }

    public string AlternativeCode { get; set; } = null!;

    public int PltAssetAlternativeCodeID { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public int GroupBankID { get; set; }

    public long? CaseID { get; set; }

    public virtual Asset Asset { get; set; } = null!;
}
