using Lawify.Common.DependencyInjection;
using Lawify.Common.Options;

namespace Lawify.Search.Api.DependencyInjection;

public static class OptionsExtension
{
    public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.BindOption<ElkOptions>(configuration, ElkOptions.Elk);
        return services;
    }
}