using Lawify.ContentDispatchingProcessor.Common.Files;
using Lawify.Messaging.Events.Contents;
using Lawify.Messaging.Events.Laws;
using MassTransit;
using MediatR;

namespace Lawify.ContentDispatchingProcessor.ProcessContent.ContentExtractors.Laws;

public record LawForExtract(FileContent Content) : INotification;

public class LawExtractHandler(
    IContentExtractor<LawExtracted> contentExtractor,
    IPublishEndpoint publishEndpoint
) : INotificationHandler<LawForExtract>
{
    public async Task Handle(LawForExtract notification, CancellationToken cancellationToken)
    {
        var law = await contentExtractor
            .ExtractContentAsync(notification.Content, cancellationToken);
        if (law.IsFailure) {
            return;
        }

        var lawMetadata = law.Value.Metadata;
        var metadata = new ContentMetadataExported(
            lawMetadata.Title,
            lawMetadata.FileName,
            lawMetadata.CreatedAt,
            lawMetadata.Author,
            lawMetadata.Category
        );
        await publishEndpoint.Publish(
            new LawExported(
                law.Value.Content,
                metadata
            ),
            cancellationToken
        );
    }
}