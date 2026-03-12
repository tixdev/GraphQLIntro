using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class AssetToRelationship
{
    public int AssetToRelationshipID { get; set; }

    public int AssetID { get; set; }

    public int RelationshipID { get; set; }

    public int PltAssetToRelationshipLinkID { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public int GroupBankID { get; set; }

    public long? CaseID { get; set; }

    public virtual Asset Asset { get; set; } = null!;
}
