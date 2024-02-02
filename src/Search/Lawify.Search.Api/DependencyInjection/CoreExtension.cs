using System.Reflection;
using Lawify.Common.Mediator;
using MediatR;

namespace Lawify.Search.Api.DependencyInjection;

public static class CoreExtension
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        return services;
    }
}