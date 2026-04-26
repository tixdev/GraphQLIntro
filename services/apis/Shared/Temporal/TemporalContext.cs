namespace Shared.Temporal;

public class TemporalContext : ITemporalContext
{
    public TemporalFilterMode Mode { get; set; } = TemporalFilterMode.AsOf;
    public DateTime? RangeStart { get; set; }
    public DateTime? RangeEnd { get; set; }

    public DateTime QueryMaxStartDate
    {
        get
        {
            return Mode switch
            {
                TemporalFilterMode.All => DateTime.MaxValue,
                TemporalFilterMode.AsOf => RangeStart ?? DateTime.MaxValue,
                TemporalFilterMode.AnyTimeIn => (RangeEnd ?? DateTime.Now).AddTicks(-1),
                TemporalFilterMode.Throughout => RangeStart ?? DateTime.MinValue,
                _ => DateTime.MaxValue
            };
        }
    }

    public DateTime QueryMinEndDate
    {
        get
        {
            return Mode switch
            {
                TemporalFilterMode.All => DateTime.MinValue,
                TemporalFilterMode.AsOf => RangeStart ?? DateTime.MaxValue.AddTicks(-1),
                TemporalFilterMode.AnyTimeIn => RangeStart ?? DateTime.MinValue,
                TemporalFilterMode.Throughout => (RangeEnd ?? DateTime.Now).AddTicks(-1),
                _ => DateTime.MinValue
            };
        }
    }
}
