using HotChocolate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace Plausibility.API.Models;

[Table("ttp_AssetToPersonLinkLanguage", Schema = "plausibility")]
[GraphQLName("PltAssetToPersonLinkTranslation")]
public partial class AssetToPersonLinkTranslation
{
    [Key]
    public int AssetToPersonLinkLanguageID { get; set; }

    public int GroupBankID { get; set; }

    public int LanguageID { get; set; }

    public int LifeCycleStateID { get; set; }

    public int AssetToPersonLinkID { get; set; }

    public string Code { get; set; } = null!;

    public string ShortDescription { get; set; } = null!;

    public string LongDescription { get; set; } = null!;

    public string ReverseCode { get; set; } = null!;

    public string ReverseDescription { get; set; } = null!;

    public string? NoteDescription { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime? ValidEndDate { get; set; }

    public virtual AssetToPersonLink AssetToPersonLink { get; set; } = null!;
}
