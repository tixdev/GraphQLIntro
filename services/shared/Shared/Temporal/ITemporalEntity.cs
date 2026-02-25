namespace Shared.Temporal;

public interface ITemporalEntity
{
    DateTime ValidStartDate { get; set; }
    DateTime ValidEndDate { get; set; }
}
