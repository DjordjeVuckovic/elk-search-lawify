using Lawify.Search.Api.MessageBroker.Extensions;

namespace Lawify.Search.Api.DependencyInjection;

public static class InfraExtensions
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddMessageBroker();

        return services;
    }
}