using HotChocolate.AspNetCore;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Temporal;

public class TemporalHttpRequestInterceptor : DefaultHttpRequestInterceptor
{
    private readonly string _headerKey = "x-as-of-date";
    // TBD: Could be injected or configured if needed, but for now we rely on Local timezone of the server.
    private readonly TimeZoneInfo _localTimeZone = TimeZoneInfo.Local;

    public override ValueTask OnCreateAsync(HttpContext context, IRequestExecutor requestExecutor, OperationRequestBuilder requestBuilder, CancellationToken cancellationToken)
    {
        var temporalContext = context.RequestServices.GetRequiredService<ITemporalContext>();

        if (context.Request.Headers.TryGetValue(_headerKey, out var headerValue) && !string.IsNullOrWhiteSpace(headerValue))
        {
            if (DateTime.TryParse(headerValue, out var parsedDate))
            {
                // Ensure parsed timezone is standardized
                if (parsedDate.Kind == DateTimeKind.Unspecified)
                {
                    // If no offset, assume it was sent as UTC
                    parsedDate = DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc);
                }

                // Convert to the local TimeZone of the database
                temporalContext.AsOfDate = TimeZoneInfo.ConvertTimeFromUtc(parsedDate.ToUniversalTime(), _localTimeZone);
            }
        }

        return base.OnCreateAsync(context, requestExecutor, requestBuilder, cancellationToken);
    }
}
