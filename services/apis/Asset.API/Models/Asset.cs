using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class Asset
{
    public int AssetID { get; set; }

    public int ProductID { get; set; }

    public int RelationshipID { get; set; }

    public int PltAssetTypeID { get; set; }

    public int Number { get; set; }

    public int GroupBankID { get; set; }

    public bool IsMigrated { get; set; }

    public virtual ICollection<AssetAlternativeCode> AssetAlternativeCodes { get; set; } = new List<AssetAlternativeCode>();

    public virtual ICollection<AssetDate> AssetDates { get; set; } = new List<AssetDate>();

    public virtual ICollection<AssetDebitCard> AssetDebitCards { get; set; } = new List<AssetDebitCard>();

    public virtual ICollection<AssetDetail> AssetDetails { get; set; } = new List<AssetDetail>();

    public virtual ICollection<AssetIntermediary> AssetIntermediaries { get; set; } = new List<AssetIntermediary>();

    public virtual ICollection<AssetLifeCycleStatus> AssetLifeCycleStatuses { get; set; } = new List<AssetLifeCycleStatus>();

    public virtual ICollection<AssetNote> AssetNotes { get; set; } = new List<AssetNote>();

    public virtual ICollection<AssetOther> AssetOthers { get; set; } = new List<AssetOther>();

    public virtual ICollection<AssetSafetyBox> AssetSafetyBoxes { get; set; } = new List<AssetSafetyBox>();

    public virtual ICollection<AssetToAsset> AssetToAssetAssetNavigations { get; set; } = new List<AssetToAsset>();

    public virtual ICollection<AssetToAsset> AssetToAssetAssets { get; set; } = new List<AssetToAsset>();

    public virtual ICollection<AssetToCondition> AssetToConditions { get; set; } = new List<AssetToCondition>();

    public virtual ICollection<AssetToPerson> AssetToPeople { get; set; } = new List<AssetToPerson>();

    public virtual ICollection<AssetToRelationship> AssetToRelationships { get; set; } = new List<AssetToRelationship>();

    public virtual ICollection<AssetVisaDebitCard> AssetVisaDebitCards { get; set; } = new List<AssetVisaDebitCard>();
}
