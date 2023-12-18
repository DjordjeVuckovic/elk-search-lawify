using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Http;

namespace Lawify.Common.Serilog;

public static class SerilogExtensions
{
    private static readonly string[] ValidEnvironments =
    {
        "dev",
        "test", 
        "prod"
    };
    
    public static IHostBuilder ConfigureSerilog(
        this IHostBuilder hostBuilder, 
        string httpSinkRequestUri, 
        string serviceName, 
        string environment)
    {
        hostBuilder.UseSerilog((_, loggerConfiguration) => {
            if (!string.IsNullOrEmpty(environment) && ValidEnvironments.Contains(environment)) {
                loggerConfiguration
                    .WriteTo.DurableHttpUsingTimeRolledBuffers(httpSinkRequestUri,
                        bufferRollingInterval: BufferRollingInterval.Month,
                        restrictedToMinimumLevel: LogEventLevel.Warning,
                        logEventsInBatchLimit: 1000)
                    .Enrich.WithProperty("environment", environment)
                    .Enrich.WithProperty("serviceName", serviceName);
            }

            loggerConfiguration
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .Destructure.ToMaximumDepth(4)
                .Destructure.ToMaximumStringLength(100)
                .Destructure.ToMaximumCollectionCount(10)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .MinimumLevel.Override("System", LogEventLevel.Error);
        });

        return hostBuilder;
    }
}