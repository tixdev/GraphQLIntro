using System;
using System.Collections.Generic;

namespace Product.API.Models;

public partial class ProductRoleAllowed
{
    public int ProductRoleAllowedId { get; set; }

    public int ProductId { get; set; }

    public int PltRoleAllowedId { get; set; }

    public bool? Molteplicity { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public int GroupBankId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
