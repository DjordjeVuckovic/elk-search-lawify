using Lawify.Common.Options;
using Lawify.Common.Serilog;
using Lawify.Search.Api.DependencyInjection.Api;
using Lawify.Search.Api.DependencyInjection.Options;
using Microsoft.Extensions.Options;

namespace Lawify.Search.Api.DependencyInjection;

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