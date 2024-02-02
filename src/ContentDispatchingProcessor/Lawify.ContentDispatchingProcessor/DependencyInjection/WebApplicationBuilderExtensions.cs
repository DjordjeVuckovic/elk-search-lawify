using Lawify.Common.Options;
using Lawify.Common.Serilog;
using Microsoft.Extensions.Options;

namespace Lawify.ContentDispatchingProcessor.DependencyInjection;

public static class WebApplicationBuilderExtensions
{
    internal static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddOptionsEnv(builder.Configuration)
            .AddApi()
            .AddInfra(builder.Configuration)
            .AddCore();

        var elkOptions = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<ElkOptions>>().Value;
        builder.Host.ConfigureSerilog(
            elkOptions.HttpSinkRequestUri,
            elkOptions.ServiceName,
            elkOptions.Environment
        );
    }
}