using Elastic.Clients.Elasticsearch;
using MediatR;

namespace Lawify.Search.Api.Features.SerbianSearch.Contracts.Feature;

public class GeospatialSearch
{
    public record Query(
        string Street,
        string City,
        string RadiusKm
    ) : IRequest<IReadOnlyCollection<SerbianContract>>;

    public class Handler(ElasticsearchClient elasticsearchClient)
        : IRequestHandler<Query, IReadOnlyCollection<SerbianContract>>
    {
        public async Task<IReadOnlyCollection<SerbianContract>> Handle(
            Query request,
            CancellationToken cancellationToken
        )
        {
            // TODO: Implement retrieving geospatial coordinates from address via LocationIQ API
            var lat = 30.34;
            var lang = 29.30;
            var response = await elasticsearchClient.SearchAsync<SerbianContract>(s =>
                s.Query(q => q
                    .GeoDistance(g => g
                        .Field(f => f.GeoLocation)
                        .Distance($"{request.RadiusKm}km")
                        .Location(
                            GeoLocation.LatitudeLongitude(
                                new LatLonGeoLocation { Lat = lat, Lon = lang }
                            )
                        )
                    )
                ), cancellationToken);

            return response.Documents;
        }
    }
}