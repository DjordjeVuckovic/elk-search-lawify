using Lawify.ContentDispatchingProcessor.Common.Files;
using Lawify.ContentDispatchingProcessor.ContentExtractors.Contracts;
using Lawify.ContentDispatchingProcessor.ContentExtractors.Laws;
using Lawify.Messaging.Events.Contents;
using MediatR;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace Lawify.ContentDispatchingProcessor.ProcessContent;

public class ContentProcessorHandler(
    IPublisher publisher,
    IMinioClient minioClient,
    ILogger<ContentProcessorHandler> logger
) : INotificationHandler<ContentReceived>
{
    public async Task Handle(ContentReceived notification, CancellationToken cancellationToken)
    {
        var fileStream = new MemoryStream();
        var getObject = new GetObjectArgs()
            .WithBucket(notification.BucketName)
            .WithObject(notification.FileName)
            .WithCallbackStream(stream => {
                stream.CopyTo(fileStream);
            });

        try {
            await minioClient.GetObjectAsync(getObject, cancellationToken);
        } catch (MinioException e) {
            logger.LogError("Error getting file: {@Message}", e.Message);
        }

        var fileContent = new FileContent(
            fileStream,
            notification.FileName,
            notification.ContentType
        );
        switch (notification.DocumentType) {
            case DocumentType.Contract:
                await publisher.Publish(new ContractForExtract(fileContent), cancellationToken);
                break;
            case DocumentType.Law:
                await publisher.Publish(new LawForExtract(fileContent), cancellationToken);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(notification.DocumentType));
        }
    }
}