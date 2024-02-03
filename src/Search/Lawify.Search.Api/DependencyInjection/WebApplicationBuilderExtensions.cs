using Lawify.Common.Options;
using Lawify.Common.Serilog;
using Lawify.Search.Api.ElasticSearch.Configuration;
using Microsoft.Extensions.Options;

namespace Lawify.Search.Api.DependencyInjection;

public static class WebApplicationBuilderExtensions
{
    internal static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddOptionsEnv(builder.Configuration)
            .AddApi()
            .AddElasticSearch()
            .AddCore()
            .AddInfra(builder.Configuration);

        var elkOptions = builder.Services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<ElkOptions>>().Value;

        builder.Host.ConfigureSerilog(
            elkOptions.HttpSinkRequestUri,
            elkOptions.ServiceName,
            elkOptions.Environment
        );
    }
}