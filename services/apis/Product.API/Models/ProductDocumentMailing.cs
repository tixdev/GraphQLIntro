using System;
using System.Collections.Generic;

namespace Product.API.Models;

public partial class ProductDocumentMailing
{
    public int ProductDocumentMailingId { get; set; }

    public int GroupBankId { get; set; }

    public int ProductId { get; set; }

    public int PltMailingDocumentTypeId { get; set; }

    public int MinEditions { get; set; }

    public int MaxEditions { get; set; }

    public int MinCopies { get; set; }

    public int MaxCopies { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public bool AutomaticManagement { get; set; }

    public virtual Product Product { get; set; } = null!;
}
