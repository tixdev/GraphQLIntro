using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class AssetToMandateForPaymentInvoice
{
    public Guid MandateID { get; set; }

    public int AssetInvoiceID { get; set; }

    public int AccountID { get; set; }

    public int GroupBankID { get; set; }
}
