using FluentValidation;
using Lawify.Common.Mediator;
using MediatR;

namespace Lawify.ContentDispatchingProcessor.DependencyInjection;

public static class CoreExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        return services;
    }
}