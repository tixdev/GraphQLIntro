using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class AssetSafetyBox
{
    public int AssetSafetyBoxID { get; set; }

    public int AssetID { get; set; }

    public DateTime? SafetyBoxValidityStartDate { get; set; }

    public DateTime? SafetyBoxValidityEndDate { get; set; }

    public DateTime RecurrenceBillingDate { get; set; }

    public decimal BailementAmount { get; set; }

    public int CurrencyID { get; set; }

    public int SealNumber { get; set; }

    public int SafetyBoxID { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public int GroupBankID { get; set; }

    public long? CaseID { get; set; }

    public virtual Asset Asset { get; set; } = null!;
}
