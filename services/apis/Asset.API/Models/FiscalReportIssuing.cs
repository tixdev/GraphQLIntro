using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class FiscalReportIssuing
{
    public int FiscalReportIssuingID { get; set; }

    public int RelationshipID { get; set; }

    public int? AssetID { get; set; }

    public DateTime IssueDate { get; set; }

    public int PltNationAlpha2ID { get; set; }

    public int FiscalYear { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public string? DocumentType { get; set; }

    public virtual FiscalReportAccounting? FiscalReportAccounting { get; set; }
}
