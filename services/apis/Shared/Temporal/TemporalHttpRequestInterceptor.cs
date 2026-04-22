using HotChocolate.AspNetCore;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Temporal;

public class TemporalHttpRequestInterceptor : DefaultHttpRequestInterceptor
{
    private readonly string _modeHeaderKey = "x-temporal-mode";
    private readonly string _rangeStartHeaderKey = "x-temporal-range-start";
    private readonly string _rangeEndHeaderKey = "x-temporal-range-end";

    public override async ValueTask OnCreateAsync(HttpContext context, IRequestExecutor requestExecutor,
        OperationRequestBuilder requestBuilder, CancellationToken cancellationToken)
    {
        var temporalContext = context.RequestServices.GetRequiredService<ITemporalContext>();

        if (context.Request.Headers.TryGetValue(_modeHeaderKey, out var modeValue) &&
            Enum.TryParse<TemporalFilterMode>(modeValue, true, out var parsedMode))
        {
            temporalContext.Mode = parsedMode;
        }
        
        if (context.Request.Headers.TryGetValue(_rangeStartHeaderKey, out var startValue) &&
            DateTime.TryParse(startValue, out var parsedStart))
        {
            temporalContext.RangeStart = ParseAndConvertDate(parsedStart);
        }

        if (context.Request.Headers.TryGetValue(_rangeEndHeaderKey, out var endValue) &&
            DateTime.TryParse(endValue, out var parsedEnd))
        {
            temporalContext.RangeEnd = ParseAndConvertDate(parsedEnd);
        }
        
        if (temporalContext.RangeEnd.HasValue)
        {
            if (!temporalContext.RangeStart.HasValue)
            {
                throw new GraphQLException(
                    "When 'x-temporal-range-end' is provided, 'x-temporal-range-start' must also be provided.");
            }

            if (temporalContext.RangeEnd.Value < temporalContext.RangeStart.Value)
            {
                throw new GraphQLException("'x-temporal-range-end' cannot be earlier than 'x-temporal-range-start'.");
            }
        }

        await base.OnCreateAsync(context, requestExecutor, requestBuilder, cancellationToken);
    }

    private static DateTime ParseAndConvertDate(DateTime parsedDate)
    {
        return parsedDate.Kind switch
        {
            DateTimeKind.Unspecified => DateTime.SpecifyKind(parsedDate, DateTimeKind.Local),
            DateTimeKind.Utc => parsedDate.ToLocalTime(),
            _ => parsedDate
        };
    }
}