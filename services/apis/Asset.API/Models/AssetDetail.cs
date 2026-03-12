using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class AssetDetail
{
    public int AssetDetailID { get; set; }

    public int AssetID { get; set; }

    public string Name { get; set; } = null!;

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public int GroupBankID { get; set; }

    public long? CaseID { get; set; }

    public virtual Asset Asset { get; set; } = null!;
}
