using System;
using System.Collections.Generic;

namespace Product.API.Models;

public partial class ProductDocumentType
{
    public int ProductDocumentTypeId { get; set; }

    public int ProductId { get; set; }

    public int PltDocumentTypeId { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public int GroupBankId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
