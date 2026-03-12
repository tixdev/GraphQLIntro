using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class AssetIntermediary
{
    public int AssetIntermediaryID { get; set; }

    public int AssetID { get; set; }

    public DateTime? ContractDate { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public int GroupBankID { get; set; }

    public long? CaseID { get; set; }

    public virtual Asset Asset { get; set; } = null!;
}
