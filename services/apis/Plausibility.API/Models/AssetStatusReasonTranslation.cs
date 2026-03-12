using HotChocolate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace Plausibility.API.Models;

[Table("ttp_AssetStatusReasonLanguage", Schema = "plausibility")]
[GraphQLName("PltAssetStatusReasonTranslation")]
public partial class AssetStatusReasonTranslation
{
    [Key]
    public int AssetStatusReasonLanguageID { get; set; }

    public int GroupBankID { get; set; }

    public int LanguageID { get; set; }

    public int LifeCycleStateID { get; set; }

    public int AssetStatusReasonID { get; set; }

    public string Code { get; set; } = null!;

    public string ShortDescription { get; set; } = null!;

    public string LongDescription { get; set; } = null!;

    public string? NoteDescription { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime? ValidEndDate { get; set; }

    public virtual AssetStatusReason AssetStatusReason { get; set; } = null!;
}
