using Lawify.Common.Options;
using Lawify.ContentDispatchingProcessor.MessageBroker.Consumers;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Lawify.ContentDispatchingProcessor.MessageBroker.Extensions;

public static class MassTransitExtension
{
    public static IServiceCollection AddMessageBroker(this IServiceCollection services) =>
        services.AddMassTransit(configurator => {
            configurator.AddConsumer(typeof(UploadContentConsumer));
            configurator.SetKebabCaseEndpointNameFormatter();
            configurator.UsingRabbitMq((context, rabbitMqConfigurator) => {
                var rabbitMqOptions = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;
                rabbitMqConfigurator.Host(rabbitMqOptions.Host, hostConfigurator => {
                    hostConfigurator.Username(rabbitMqOptions.Username);
                    hostConfigurator.Password(rabbitMqOptions.Password);
                });
                rabbitMqConfigurator.ConfigureEndpoints(context);
            });
        });
}