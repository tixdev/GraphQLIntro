using System;
using System.Collections.Generic;

namespace Product.API.Models;

public partial class ProductToProduct
{
    public int ProductToProductId { get; set; }

    public int ProductId { get; set; }

    public int ProductChildId { get; set; }

    public int PltRoleId { get; set; }

    public int PltModeId { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public int GroupBankId { get; set; }

    public int? ParentProductId { get; set; }

    public int? PltAssetToAssetLinkId { get; set; }

    public virtual Product? ParentProduct { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Product ProductChild { get; set; } = null!;
}
