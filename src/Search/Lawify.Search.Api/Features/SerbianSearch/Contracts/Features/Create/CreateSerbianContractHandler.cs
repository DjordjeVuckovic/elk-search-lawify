using Elastic.Clients.Elasticsearch;
using Lawify.Search.Api.Features.SerbianSearch.Contracts.DomainEvents;
using Lawify.Search.Api.Features.SerbianSearch.Contracts.Factories;
using Lawify.Search.Api.LocationSearch.LocationIq;
using MediatR;

namespace Lawify.Search.Api.Features.SerbianSearch.Contracts.Features.Create;

public class CreateSerbianContractHandler(
    ElasticsearchClient elasticsearchClient,
    ILocationClient locationClient,
    ILogger<CreateSerbianContractHandler> logger) : INotificationHandler<SerbianContractReceived>
{
    public async Task Handle(SerbianContractReceived notification, CancellationToken cancellationToken)
    {
        var address = new Address(
            notification.Contract.Government?.Address!.Number!,
            notification.Contract.Government?.Address!.Street!,
            notification.Contract.Government?.Address!.City!,
            "Serbia");
        var locationResult = await locationClient.GetAddressLocation(address);
        if (!locationResult.IsSuccess) {
            logger.LogError("Failed to fetch geolocation location for address {@Address}", address);
        }

        var geoLocation = locationResult.IsSuccess
            ? new LatLonGeoLocation { Lat = locationResult.Value.Latitude, Lon = locationResult.Value.Longitude }
            : new LatLonGeoLocation { Lat = 0, Lon = 0 };

        var contract = SerbianContractFactory.ToDomain(notification.Contract, geoLocation);
        var response = await elasticsearchClient
            .IndexAsync(contract, cancellationToken);
        if (!response.IsValidResponse) {
            logger.LogError("Failed to create contract {@Contract}", contract);
        }
    }
}