using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class AssetDebitCard
{
    public int AssetDebitCardID { get; set; }

    public int AssetID { get; set; }

    public int Number { get; set; }

    public int ProgressiveNumber { get; set; }

    public string Header1 { get; set; } = null!;

    public string Header2 { get; set; } = null!;

    public DateTime FirstOpeningDate { get; set; }

    public DateTime CardValidityStartDate { get; set; }

    public DateTime CardValidityEndDate { get; set; }

    public int CurrencyID { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public int GroupBankID { get; set; }

    public long? CaseID { get; set; }

    public virtual Asset Asset { get; set; } = null!;

    public virtual ICollection<AssetDebitCardLimit> AssetDebitCardLimits { get; set; } = new List<AssetDebitCardLimit>();
}
