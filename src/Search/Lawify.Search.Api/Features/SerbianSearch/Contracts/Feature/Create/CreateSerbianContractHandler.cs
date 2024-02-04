using Elastic.Clients.Elasticsearch;
using Lawify.Search.Api.Features.SerbianSearch.Contracts.DomainEvents;
using Lawify.Search.Api.Features.SerbianSearch.Contracts.Factories;
using MediatR;

namespace Lawify.Search.Api.Features.SerbianSearch.Contracts.Feature.Create;

public class CreateSerbianContractHandler(
    ElasticsearchClient elasticsearchClient,
    ILogger<CreateSerbianContractHandler> logger) : INotificationHandler<SerbianContractReceived>
{
    public async Task Handle(SerbianContractReceived notification, CancellationToken cancellationToken)
    {
        var contract = SerbianContractFactory.ToDomain(notification.Contract, null);
        var response = await elasticsearchClient
            .IndexAsync(contract, cancellationToken);
        if (!response.IsValidResponse) {
            logger.LogError("Failed to create contract {@Contract}", contract);
        }
    }
}