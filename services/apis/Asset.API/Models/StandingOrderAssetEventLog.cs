using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class StandingOrderAssetEventLog
{
    public Guid StandingOrderID { get; set; }

    public string Event { get; set; } = null!;

    public DateTime When { get; set; }

    public int GroupBankID { get; set; }
}
