using System;
using System.Collections.Generic;

namespace Product.API.Models;

public partial class ProductGroup
{
    public int ProductGroupId { get; set; }

    public string ProductGroupCode { get; set; } = null!;

    public int GroupBankId { get; set; }

    public virtual ICollection<ProductGroupDescription> ProductGroupDescription { get; set; } = new List<ProductGroupDescription>();

    public virtual ICollection<ProductGroupDetail> ProductGroupDetail { get; set; } = new List<ProductGroupDetail>();

    public virtual ICollection<ProductGroupLifeCycleStatus> ProductGroupLifeCycleStatus { get; set; } = new List<ProductGroupLifeCycleStatus>();

    public virtual ICollection<ProductGroupToProduct> ProductGroupToProduct { get; set; } = new List<ProductGroupToProduct>();
}
