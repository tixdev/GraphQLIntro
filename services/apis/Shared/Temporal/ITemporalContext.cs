namespace Shared.Temporal;

public interface ITemporalContext
{
    DateTime? AsOfDate { get; set; }
    DateTime CurrentAsOfDate { get; }
}
