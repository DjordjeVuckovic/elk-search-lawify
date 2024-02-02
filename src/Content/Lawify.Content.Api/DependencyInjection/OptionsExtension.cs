using Lawify.Common.DependencyInjection;
using Lawify.Common.Options;
using Lawify.Content.Api.Configuration;

namespace Lawify.Content.Api.DependencyInjection;

public static class OptionsExtension
{
    public static IServiceCollection AddOptionsEnv(this IServiceCollection services, IConfiguration configuration)
    {
        services.BindOption<ElkOptions>(configuration, ElkOptions.Elk);
        services.BindOption<RabbitMqOptions>(configuration, RabbitMqOptions.RabbitMq);
        services.BindOption<MinioOptions>(configuration, MinioOptions.Minio);
        return services;
    }
}