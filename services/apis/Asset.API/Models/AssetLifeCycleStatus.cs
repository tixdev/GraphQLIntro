using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class AssetLifeCycleStatus
{
    public int AssetLifeCycleStatusID { get; set; }

    public int AssetID { get; set; }

    public int PltAssetStatusID { get; set; }

    public int? PltAssetStatusReasonID { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public int GroupBankID { get; set; }

    public long? CaseID { get; set; }

    public virtual Asset Asset { get; set; } = null!;
}
