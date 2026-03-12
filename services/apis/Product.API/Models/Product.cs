using System;
using System.Collections.Generic;

namespace Product.API.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductCode { get; set; } = null!;

    public int GroupBankId { get; set; }

    public virtual ICollection<ProductDescription> ProductDescription { get; set; } = new List<ProductDescription>();

    public virtual ICollection<ProductDetail> ProductDetail { get; set; } = new List<ProductDetail>();

    public virtual ICollection<ProductDocument> ProductDocument { get; set; } = new List<ProductDocument>();

    public virtual ICollection<ProductDocumentMailing> ProductDocumentMailing { get; set; } = new List<ProductDocumentMailing>();

    public virtual ICollection<ProductDocumentType> ProductDocumentType { get; set; } = new List<ProductDocumentType>();

    public virtual ICollection<ProductGroupToProduct> ProductGroupToProduct { get; set; } = new List<ProductGroupToProduct>();

    public virtual ICollection<ProductLifeCycleStatus> ProductLifeCycleStatus { get; set; } = new List<ProductLifeCycleStatus>();

    public virtual ICollection<ProductRoleAllowed> ProductRoleAllowed { get; set; } = new List<ProductRoleAllowed>();

    public virtual ICollection<ProductToCondition> ProductToCondition { get; set; } = new List<ProductToCondition>();

    public virtual ICollection<ProductToProduct> ProductToProductParentProduct { get; set; } = new List<ProductToProduct>();

    public virtual ICollection<ProductToProduct> ProductToProductProduct { get; set; } = new List<ProductToProduct>();

    public virtual ICollection<ProductToProduct> ProductToProductProductChild { get; set; } = new List<ProductToProduct>();
}
