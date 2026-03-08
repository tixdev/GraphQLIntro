namespace Shared.Temporal;

public class TemporalContext : ITemporalContext
{
    public DateTime CurrentAsOfDate => DateTime.Now;
    public TemporalFilterMode Mode { get; set; } = TemporalFilterMode.AsOf;
    public DateTime? RangeStart { get; set; }
    public DateTime? RangeEnd { get; set; }
    public bool IsRangeStartProvided => RangeStart.HasValue;
    public DateTime SafeRangeStart => RangeStart ?? DateTime.MinValue;
    public DateTime SafeRangeEnd => RangeEnd ?? DateTime.Now;
}
