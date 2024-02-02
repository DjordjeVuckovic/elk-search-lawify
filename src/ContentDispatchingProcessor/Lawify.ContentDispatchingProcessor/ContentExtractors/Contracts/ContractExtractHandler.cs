using Lawify.ContentDispatchingProcessor.Common.Files;
using MediatR;

namespace Lawify.ContentDispatchingProcessor.ContentExtractors.Contracts;

public record ContractForExtract(FileContent FileContent) : INotification;

public class ContractExtractorHandler(
    IContentExtractor<Contract> contentExtractor
    ) : INotificationHandler<ContractForExtract>
{
    public Task Handle(ContractForExtract notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}