using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class AssetDebitCardLimit
{
    public int AssetDebitCardLimitID { get; set; }

    public int AssetDebitCardID { get; set; }

    public int PltDebitCardLimitID { get; set; }

    public decimal LimitAmount { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public int GroupBankID { get; set; }

    public long? CaseID { get; set; }

    public virtual AssetDebitCard AssetDebitCard { get; set; } = null!;
}
