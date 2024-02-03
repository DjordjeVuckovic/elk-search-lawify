using Lawify.Common.DependencyInjection;
using Lawify.Common.Options;
using Lawify.Search.Api.ElasticSearch.Configuration;

namespace Lawify.Search.Api.DependencyInjection;

public static class OptionsExtension
{
    public static IServiceCollection AddOptionsEnv(this IServiceCollection services, IConfiguration configuration)
    {
        services.BindOption<ElkOptions>(configuration, ElkOptions.Elk);
        services.BindOption<ElasticSearchOptions>(configuration, ElasticSearchOptions.Els);
        services.BindOption<RabbitMqOptions>(configuration, RabbitMqOptions.RabbitMq);
        return services;
    }
}