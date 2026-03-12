using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class FiscalReportAccounting
{
    public int FiscalReportAccountingID { get; set; }

    public int FiscalReportIssuingID { get; set; }

    public decimal ChargeAmount { get; set; }

    public string IsoCode { get; set; } = null!;

    public string? ExternalKey { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public virtual FiscalReportIssuing FiscalReportIssuing { get; set; } = null!;
}
