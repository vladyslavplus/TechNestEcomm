using Serilog;
using Serilog.Formatting.Json;

namespace TechNest.WebApi;

public static class Extensions
{
    public static void SerilogConfiguration(this IHostBuilder host)
    {
        host.UseSerilog((context, loggerConfig) =>
        {
            loggerConfig
                .ReadFrom.Configuration(context.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId()
                .WriteTo.Console()
                .WriteTo.File(
                    new JsonFormatter(renderMessage: true), 
                    path: "Logs/logs-.txt", 
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 14,
                    shared: true
                );
        });
    }
}