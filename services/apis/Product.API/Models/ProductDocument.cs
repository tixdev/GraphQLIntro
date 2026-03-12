using System;
using System.Collections.Generic;

namespace Product.API.Models;

public partial class ProductDocument
{
    public int ProductDocumentId { get; set; }

    public int ProductId { get; set; }

    public int PltFormId { get; set; }

    public int PltFormUseCaseId { get; set; }

    public string? UseCaseGridRowCommand { get; set; }

    public int PltOptionId { get; set; }

    public int ApplicabilityIfSigned { get; set; }

    public int ApplicabilityIfMigrated { get; set; }

    public int PltArchiveId { get; set; }

    public int CopyNumber { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public int GroupBankId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
