using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class AssetToDirectDebit
{
    public Guid DDAConfigurationID { get; set; }

    public int AssetDirectDebitID { get; set; }

    public int GroupBankID { get; set; }
}
