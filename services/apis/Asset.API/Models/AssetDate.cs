using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class AssetDate
{
    public int AssetDateID { get; set; }

    public int AssetID { get; set; }

    public int PltBusinessDateTypeID { get; set; }

    public DateTime Date { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public int GroupBankID { get; set; }

    public long? CaseID { get; set; }

    public virtual Asset Asset { get; set; } = null!;
}
