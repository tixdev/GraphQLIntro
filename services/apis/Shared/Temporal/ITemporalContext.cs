namespace Shared.Temporal;

public interface ITemporalContext
{
    DateTime CurrentAsOfDate { get; }
    TemporalFilterMode Mode { get; set; }
    DateTime? RangeStart { get; set; }
    DateTime? RangeEnd { get; set; }
    bool IsRangeStartProvided { get; }
    DateTime SafeRangeStart { get; }
    DateTime SafeRangeEnd { get; }
}
