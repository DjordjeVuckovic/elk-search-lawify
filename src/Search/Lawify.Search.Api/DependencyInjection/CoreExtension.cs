using System.Reflection;

namespace Lawify.Search.Api.DependencyInjection;

public static class CoreExtension
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}