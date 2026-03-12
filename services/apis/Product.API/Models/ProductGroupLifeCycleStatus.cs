using System;
using System.Collections.Generic;

namespace Product.API.Models;

public partial class ProductGroupLifeCycleStatus
{
    public int ProductGroupLifeCycleStatusId { get; set; }

    public int ProductGroupId { get; set; }

    public int PltProductGroupStatusId { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public int GroupBankId { get; set; }

    public virtual ProductGroup ProductGroup { get; set; } = null!;
}
