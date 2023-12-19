using Lawify.Common.Options;
using Lawify.Common.Serilog;
using Lawify.Content.Api.DependencyInjection.Api;
using Lawify.Content.Api.DependencyInjection.Options;
using Microsoft.Extensions.Options;

namespace Lawify.Content.Api.DependencyInjection;

public static class WebApplicationBuilderExtensions
{
    internal static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddOptionsEnv(builder.Configuration)
            .AddApi();

        var elkOptions = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<ElkOptions>>().Value;
        builder.Host.ConfigureSerilog(
            elkOptions.HttpSinkRequestUri,
            elkOptions.ServiceName,
            elkOptions.Environment
        );
    }
}