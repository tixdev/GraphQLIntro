using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Shared.Extensions;

public static class OpenTelemetryExtensions
{
    public static IServiceCollection AddApiServiceOpenTelemetry(this IServiceCollection services, string serviceName)
    {
        var resourceBuilder = ResourceBuilder.CreateDefault()
            .AddService(serviceName);

        services.AddOpenTelemetry()
            .WithTracing(tracing =>
            {
                tracing
                    .SetResourceBuilder(resourceBuilder)
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    //.AddHotChocolateInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation(options => options.SetDbStatementForText = true)
                    .AddOtlpExporter();
                //.AddConsoleExporter();
            })
            .WithMetrics(metrics =>
            {
                metrics
                    .SetResourceBuilder(resourceBuilder)
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddOtlpExporter();
                //.AddConsoleExporter();
            });

        return services;
    }
}
