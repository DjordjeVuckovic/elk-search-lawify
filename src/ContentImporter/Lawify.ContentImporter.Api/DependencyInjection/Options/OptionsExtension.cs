using Lawify.Common.DependencyInjection;
using Lawify.Common.Options;

namespace Lawify.ContentImporter.Api.DependencyInjection.Options;

public static class OptionsExtension
{
    public static IServiceCollection AddOptionsEnv(this IServiceCollection services, IConfiguration configuration)
    {
        services.BindOption<ElkOptions>(configuration, ElkOptions.Elk);
        return services;
    }
}