using System;
using System.Collections.Generic;

namespace Product.API.Models;

public partial class ProductDetail
{
    public int ProductDetailId { get; set; }

    public int ProductId { get; set; }

    public int PltStructureId { get; set; }

    public int PltClassId { get; set; }

    public int PltSubclassId { get; set; }

    public int PltFamilyId { get; set; }

    public int PltMarketId { get; set; }

    public bool? Molteplicity { get; set; }

    public DateTime? SellStartDate { get; set; }

    public string Notes { get; set; } = null!;

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public int GroupBankId { get; set; }

    public int? PltAreaId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
