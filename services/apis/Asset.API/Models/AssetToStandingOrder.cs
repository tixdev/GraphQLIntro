using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class AssetToStandingOrder
{
    public Guid StandingOrderID { get; set; }

    public int AssetStandordID { get; set; }

    public int AccountID { get; set; }

    public bool WorkflowStarted { get; set; }

    public int GroupBankID { get; set; }
}
