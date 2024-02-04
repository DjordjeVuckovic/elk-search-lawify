using Lawify.ContentDispatchingProcessor.Common.Files;
using Lawify.ContentDispatchingProcessor.ProcessContent.ContentExtractors.Contracts.Factories;
using Lawify.ContentDispatchingProcessor.ProcessContent.ContentExtractors.Contracts.Models;
using MassTransit;
using MediatR;

namespace Lawify.ContentDispatchingProcessor.ProcessContent.ContentExtractors.Contracts;

public record ContractForExtract(FileContent FileContent) : INotification;

public class ContractExtractorHandler(
    IContentExtractor<ContractExtracted> contentExtractor,
    ILogger<ContractExtractorHandler> logger,
    IPublishEndpoint publishEndpoint
    ) : INotificationHandler<ContractForExtract>
{
    public async Task Handle(ContractForExtract notification, CancellationToken cancellationToken)
    {
        var contentResult = await contentExtractor
            .ExtractContentAsync(notification.FileContent, cancellationToken);
        if (contentResult.IsFailure) {
            logger.LogError("Error extracting content for {FileName}", notification.FileContent.FileName);
            return;
        }
        var content = contentResult.Value!;

        var exportedEvent = ContractFactory.CreateContractExported(content, notification.FileContent.FileName);
        await publishEndpoint.Publish(exportedEvent, cancellationToken);
    }
}