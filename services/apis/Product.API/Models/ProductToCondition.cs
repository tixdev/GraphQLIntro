using System;
using System.Collections.Generic;

namespace Product.API.Models;

public partial class ProductToCondition
{
    public int ProductToConditionId { get; set; }

    public int ProductId { get; set; }

    public int ConditionId { get; set; }

    public int? ConditionValueId { get; set; }

    public int? PltApplicabilityId { get; set; }

    public int? PltObligatorinessId { get; set; }

    public bool Modifiability { get; set; }

    public int Ordinal { get; set; }

    public int OrdinalType { get; set; }

    public int GroupBankId { get; set; }

    public DateTime BusinessStartDate { get; set; }

    public DateTime BusinessEndDate { get; set; }

    public DateTime ValidStartDate { get; set; }

    public DateTime ValidEndDate { get; set; }

    public int? CaseId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
