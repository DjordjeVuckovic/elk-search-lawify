using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Lawify.Common.DependencyInjection;

public static class OptionsExtension
{
    public static IServiceCollection BindOption<T>(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionName) where T : class, IValidateOptions<T>, new()
    {
        services.AddSingleton<IValidateOptions<T>, T>()
            .AddOptions<T>()
            .Bind(configuration.GetSection(sectionName))
            .ValidateOnStart();
        return services;
    }
}