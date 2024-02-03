using Lawify.Messaging.Events.Laws;
using Lawify.Search.Api.Features.SerbianSearch.Laws.DomainEvents;
using MassTransit;
using MediatR;

namespace Lawify.Search.Api.MessageBroker.Consumers;

public class LawExportedConsumer(
    IPublisher publisher,
    ILogger<LawExportedConsumer> logger) : IConsumer<LawExported>
{
    public async Task Consume(ConsumeContext<LawExported> context)
    {
        var law = context.Message;
        logger.LogInformation("Received law {@Law}", law);
        await publisher.Publish(
            new LawReceived(
                law.Content,
                new LawReceivedMetadata(
                    law.ExportedMetadata.Title,
                    law.ExportedMetadata.FileName,
                    law.ExportedMetadata.CreatedAt,
                    law.ExportedMetadata.Author,
                    law.ExportedMetadata.Category
                )
            ),
            cancellationToken: context.CancellationToken
        );
    }
}