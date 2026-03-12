using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class AssetBusinessVisaDebitCardRequest
{
    public int BusinessVisaDebitID { get; set; }

    public int RelationshipId { get; set; }

    public int RelationshipNumber { get; set; }

    public int PersonId { get; set; }

    public string PersonNumber { get; set; } = null!;

    public int AssetId { get; set; }

    public decimal? Limit { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? ProcessedDate { get; set; }

    public string Channel { get; set; } = null!;
}
