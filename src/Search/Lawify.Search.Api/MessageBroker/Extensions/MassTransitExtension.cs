using Lawify.Common.Options;
using Lawify.Search.Api.MessageBroker.Consumers;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Lawify.Search.Api.MessageBroker.Extensions;

public static class MassTransitExtension
{
    private static void AddConsumers(IRegistrationConfigurator configurator)
    {
        configurator.AddConsumer(typeof(LawExportedConsumer));
        configurator.AddConsumer(typeof(SerbianContractExportedConsumer));
    }
    public static IServiceCollection AddMessageBroker(this IServiceCollection services) =>
        services.AddMassTransit(configurator => {
            AddConsumers(configurator);
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