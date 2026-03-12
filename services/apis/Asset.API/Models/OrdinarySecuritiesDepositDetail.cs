using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class OrdinarySecuritiesDepositDetail
{
    public int AssetID { get; set; }

    public DateOnly ValueDate { get; set; }

    public string AccountCurrency { get; set; } = null!;

    public decimal? TotalEquity { get; set; }

    public DateTime? Timestamp { get; set; }
}
