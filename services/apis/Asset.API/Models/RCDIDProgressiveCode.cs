using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class RCDIDProgressiveCode
{
    public string Channel { get; set; } = null!;

    public string RCDID { get; set; } = null!;

    public string? MaestroCardNumber { get; set; }

    public DateTime? TakenDate { get; set; }
}
