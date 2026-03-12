using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class AssetVisaDebitCard
{
    public int AssetVisaDebitCardID { get; set; }

    public int AssetID { get; set; }

    public string CornerCardID { get; set; } = null!;

    public bool Migrated { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public int GroupBankID { get; set; }

    public long? CaseID { get; set; }

    public virtual Asset Asset { get; set; } = null!;
}
