using Lawify.Common.DependencyInjection;
using Lawify.Common.Options;
using Lawify.Search.Api.ElasticSearch;
using Lawify.Search.Api.ElasticSearch.Configuration;

namespace Lawify.Search.Api.DependencyInjection.Options;

public static class OptionsExtension
{
    public static IServiceCollection AddOptionsEnv(this IServiceCollection services, IConfiguration configuration)
    {
        services.BindOption<ElkOptions>(configuration, ElkOptions.Elk);
        services.BindOption<ElasticSearchOptions>(configuration, ElasticSearchOptions.Els);
        return services;
    }
}