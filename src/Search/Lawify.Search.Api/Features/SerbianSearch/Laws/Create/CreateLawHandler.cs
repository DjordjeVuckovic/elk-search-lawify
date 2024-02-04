using Elastic.Clients.Elasticsearch;
using Lawify.Search.Api.Features.SerbianSearch.Laws.DomainEvents;
using MediatR;

namespace Lawify.Search.Api.Features.SerbianSearch.Laws.Create;

public class CreateLawHandler(
    ElasticsearchClient elasticsearchClient,
    ILogger<CreateLawHandler> logger
) : INotificationHandler<LawReceived>
{
    public async Task Handle(LawReceived notification, CancellationToken cancellationToken)
    {
        var law = SerbianLaw.Create(
            notification.Content,
            notification.Metadata.Title,
            notification.Metadata.FileName,
            notification.Metadata.CreatedAt,
            notification.Metadata.Category,
            notification.Metadata.Author
        );
        var response = await elasticsearchClient.IndexAsync(law, cancellationToken);
        if (!response.IsValidResponse) {
            logger.LogError("Failed to create law: {DebugInformation}", response.DebugInformation);
        }
    }
}