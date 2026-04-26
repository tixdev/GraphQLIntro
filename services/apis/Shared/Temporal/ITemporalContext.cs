namespace Shared.Temporal;

public interface ITemporalContext
{
    TemporalFilterMode Mode { get; set; }
    DateTime? RangeStart { get; set; }
    DateTime? RangeEnd { get; set; }

    DateTime QueryMaxStartDate { get; }
    DateTime QueryMinEndDate { get; }
}
