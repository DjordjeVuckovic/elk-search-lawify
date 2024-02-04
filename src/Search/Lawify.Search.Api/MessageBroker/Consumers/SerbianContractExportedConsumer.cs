using Lawify.Messaging.Events.Contracts;
using Lawify.Search.Api.Features.SerbianSearch.Contracts.DomainEvents;
using MassTransit;
using MediatR;

namespace Lawify.Search.Api.MessageBroker.Consumers;

public class SerbianContractExportedConsumer(
    IPublisher publisher, ILogger<SerbianContractExportedConsumer> logger) : IConsumer<SerbianContractExported>
{
    public async Task Consume(ConsumeContext<SerbianContractExported> context)
    {
        var message = context.Message;
        logger.LogInformation("Received contract exported event {@Message}", message);

        await publisher.Publish(new SerbianContractReceived {
            Contract = message
        }, context.CancellationToken);
    }
}