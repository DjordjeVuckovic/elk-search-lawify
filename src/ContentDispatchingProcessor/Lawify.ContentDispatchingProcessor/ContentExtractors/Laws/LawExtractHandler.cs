using Lawify.ContentDispatchingProcessor.Common.Files;
using Lawify.Messaging.Events.Laws;
using MassTransit;
using MediatR;

namespace Lawify.ContentDispatchingProcessor.ContentExtractors.Laws;

public record LawForExtract(FileContent Content) : INotification;

public class LawExtractHandler(
    IContentExtractor<Law> contentExtractor,
    IPublishEndpoint publishEndpoint
) : INotificationHandler<LawForExtract>
{
    public async Task Handle(LawForExtract notification, CancellationToken cancellationToken)
    {
        var law = await contentExtractor.ExtractContentAsync(notification.Content, cancellationToken);
        if (law.IsFailure) {
            return;
        }
        await publishEndpoint.Publish(new LawExported(law.Value.Content), cancellationToken);
    }
}