using HotChocolate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace Plausibility.API.Models;

[Table("ttp_AssetToRelationshipLinkLanguage", Schema = "plausibility")]
[GraphQLName("PltAssetToRelationshipLinkTranslation")]
public partial class AssetToRelationshipLinkTranslation
{
    [Key]
    public int AssetToRelationshipLinkLanguageID { get; set; }

    public int GroupBankID { get; set; }

    public int LanguageID { get; set; }

    public int LifeCycleStateID { get; set; }

    public int AssetToRelationshipLinkID { get; set; }

    public string Code { get; set; } = null!;

    public string ShortDescription { get; set; } = null!;

    public string LongDescription { get; set; } = null!;

    public string ReverseCode { get; set; } = null!;

    public string ReverseDescription { get; set; } = null!;

    public string? NoteDescription { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime? ValidEndDate { get; set; }

    public virtual AssetToRelationshipLink AssetToRelationshipLink { get; set; } = null!;
}
