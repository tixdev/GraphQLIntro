using Microsoft.EntityFrameworkCore;

namespace Shared.Temporal;

public interface ITemporalDbContext
{
    ITemporalContext TemporalContext { get; }
}
