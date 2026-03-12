using System;
using System.Collections.Generic;

namespace Product.API.Models;

public partial class ProductDescription
{
    public int ProductDescriptionId { get; set; }

    public int ProductId { get; set; }

    public string Description { get; set; } = null!;

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public int LanguageId { get; set; }

    public int GroupBankId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
