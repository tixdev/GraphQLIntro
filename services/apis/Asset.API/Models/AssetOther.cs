using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class AssetOther
{
    public int AssetOtherID { get; set; }

    public int AssetID { get; set; }

    public DateTime? OthersValidityStartDate { get; set; }

    public DateTime? OthersValidityEndDate { get; set; }

    public decimal Amount { get; set; }

    public int CurrencyID { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public int GroupBankID { get; set; }

    public long? CaseID { get; set; }

    public virtual Asset Asset { get; set; } = null!;
}
