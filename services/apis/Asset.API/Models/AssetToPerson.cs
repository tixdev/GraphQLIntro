using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class AssetToPerson
{
    public int AssetToPersonID { get; set; }

    public int AssetID { get; set; }

    public int PersonID { get; set; }

    public int PltAssetToPersonLinkID { get; set; }

    public int? PltPowerOfSignatureID { get; set; }

    public int? SignatoriesNumber { get; set; }

    public string? Instructions { get; set; }

    public int? OnBehalfOf { get; set; }

    public DateOnly? PowerStartDate { get; set; }

    public DateOnly? PowerEndDate { get; set; }

    public string? DeleteReason { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public int GroupBankID { get; set; }

    public long? CaseID { get; set; }

    public virtual Asset Asset { get; set; } = null!;
}
