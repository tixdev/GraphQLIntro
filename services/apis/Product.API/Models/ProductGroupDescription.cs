using System;
using System.Collections.Generic;

namespace Product.API.Models;

public partial class ProductGroupDescription
{
    public int ProductGroupDescriptionId { get; set; }

    public int ProductGroupId { get; set; }

    public string Description { get; set; } = null!;

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public int LanguageId { get; set; }

    public int GroupBankId { get; set; }

    public virtual ProductGroup ProductGroup { get; set; } = null!;
}
