using System;
using System.Collections.Generic;

namespace Plausibility.API.Models;

public partial class ttp_DebitCardLimitLanguage
{
    public int DebitCardLimitLanguageID { get; set; }

    public int GroupBankID { get; set; }

    public int LanguageID { get; set; }

    public int LifeCycleStateID { get; set; }

    public int DebitCardLimitID { get; set; }

    public string Code { get; set; } = null!;

    public string ShortDescription { get; set; } = null!;

    public string LongDescription { get; set; } = null!;

    public string? NoteDescription { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime? ValidEndDate { get; set; }

    public virtual tcd_DebitCardLimit DebitCardLimit { get; set; } = null!;
}
