using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class AssetToCondition
{
    public int AssetToConditionID { get; set; }

    public int AssetID { get; set; }

    public int ConditionID { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public int GroupBankID { get; set; }

    public int? CodeID { get; set; }

    public int? CodeValueID { get; set; }

    public long? CaseID { get; set; }

    public virtual Asset Asset { get; set; } = null!;
}
