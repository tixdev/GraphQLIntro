using System;
using System.Collections.Generic;

namespace Product.API.Models;

public partial class ProductGroupToProduct
{
    public int ProductGroupToProductId { get; set; }

    public int ProductGroupId { get; set; }

    public int ProductId { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public int GroupBankId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ProductGroup ProductGroup { get; set; } = null!;
}
