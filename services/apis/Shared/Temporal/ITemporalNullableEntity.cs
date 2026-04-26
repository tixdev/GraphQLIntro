namespace Shared.Temporal;

public interface ITemporalNullableEntity
{
    DateTime ValidStartDate { get; set; }
    DateTime? ValidEndDate { get; set; }
}
