using Lawify.Content.Api.Configuration;
using Lawify.Content.Api.MessageBroker;
using Minio;

namespace Lawify.Content.Api.DependencyInjection;

public static class InfraExtension
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMessageBroker();
        services.AddMinio(x => {
            var options = configuration
                .GetSection(MinioOptions.Minio)
                .Get<MinioOptions>()!;
            x.WithCredentials(options.AccessKey, options.SecretKey);
            x.WithEndpoint(options.Endpoint);
            x.WithSSL(false);
        });

        return services;
    }
}