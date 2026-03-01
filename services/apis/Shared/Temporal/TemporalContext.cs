namespace Shared.Temporal;

public class TemporalContext : ITemporalContext
{
    public DateTime? AsOfDate { get; set; }
    public DateTime CurrentAsOfDate => AsOfDate ?? DateTime.Now;
}
