using System.Reflection;

namespace Lawify.Search.Api.DependencyInjection.Mediator;

public static class MediatorExtension
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}