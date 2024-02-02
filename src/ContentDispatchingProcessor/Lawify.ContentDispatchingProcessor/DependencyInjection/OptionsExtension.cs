using Lawify.Common.DependencyInjection;
using Lawify.Common.Options;
using Lawify.Content.Api.Configuration;
using Lawify.ContentDispatchingProcessor.Configuration;

namespace Lawify.ContentDispatchingProcessor.DependencyInjection;

public static class OptionsExtension
{
    public static IServiceCollection AddOptionsEnv(this IServiceCollection services, IConfiguration configuration)
    {
        services.BindOption<ElkOptions>(configuration, ElkOptions.Elk);
        services.BindOption<RabbitMqOptions>(configuration, RabbitMqOptions.RabbitMq);
        services.BindOption<MinioOptions>(configuration, MinioOptions.Minio);
        services.BindOption<PdfOptions>(configuration, PdfOptions.Pdf);
        return services;
    }
}