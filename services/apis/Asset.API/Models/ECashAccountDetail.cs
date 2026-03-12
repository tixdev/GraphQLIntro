using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class ECashAccountDetail
{
    public int ECashAccountDetailID { get; set; }

    public string DinitCode { get; set; } = null!;

    public int RelationshipNumber { get; set; }

    public int AccountNumber { get; set; }

    public DateTime ReferenceDate { get; set; }

    public DateTime DisbursementDate { get; set; }

    public DateTime FirstInstallmentDate { get; set; }

    public int LoanPeriod { get; set; }

    public DateTime EndDate { get; set; }

    public decimal InitialAmount { get; set; }

    public decimal AmortizationAmount { get; set; }

    public decimal CumulativeUnpaidAmount { get; set; }

    public DateTime ValidStartDate { get; set; }

    public decimal CumulativeUnpaidPrincipalAmount { get; set; }

    public decimal CumulativeUnpaidPPIAmount { get; set; }

    public decimal CumulativeUnpaidInterestAmount { get; set; }

    public decimal CumulativeUnpaidFeeAmount { get; set; }

    public decimal RemainingPrincipalAmount { get; set; }
}
