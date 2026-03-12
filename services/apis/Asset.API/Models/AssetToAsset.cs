using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class AssetToAsset
{
    public int AssetToAssetID { get; set; }

    public int AssetID { get; set; }

    public int AssetChildID { get; set; }

    public int PltAssetToAssetLinkID { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public int GroupBankID { get; set; }

    public long? CaseID { get; set; }

    public virtual Asset Asset { get; set; } = null!;

    public virtual Asset AssetNavigation { get; set; } = null!;
}
