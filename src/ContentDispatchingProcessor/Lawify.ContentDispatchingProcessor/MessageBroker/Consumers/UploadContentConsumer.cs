using Lawify.ContentDispatchingProcessor.ProcessContent;
using Lawify.Messaging.Events.Contents;
using MassTransit;
using MediatR;

namespace Lawify.ContentDispatchingProcessor.MessageBroker.Consumers;

public class UploadContentConsumer(
    ILogger<UploadContentConsumer> logger,
    IPublisher mediator) : IConsumer<UploadedContent>
{
    public async Task Consume(ConsumeContext<UploadedContent> context)
    {
        await Task.CompletedTask;
        var message = context.Message;
        logger.LogInformation("Message received: {@Message}", message);
        await mediator.Publish(
            new ContentReceived(
                message.FileName,
                message.BucketName,
                message.DocumentType,
                message.ContentType
            )
        );
    }
}