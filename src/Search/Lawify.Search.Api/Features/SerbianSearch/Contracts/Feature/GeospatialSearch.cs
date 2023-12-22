using Elastic.Clients.Elasticsearch;
using MediatR;

namespace Lawify.Search.Api.Features.SerbianSearch.Contracts.Feature;

public class GeospatialSearch
{
    public record Query(
        string Street,
        string City,
        string Radius
    ) : IRequest<IReadOnlyCollection<Contract>>;

    public class Handler(ElasticsearchClient elasticsearchClient)
        : IRequestHandler<Query, IReadOnlyCollection<Contract>>
    {
        public async Task<IReadOnlyCollection<Contract>> Handle(
            Query request,
            CancellationToken cancellationToken
        )
        {
            var response = await elasticsearchClient.SearchAsync<Contract>(s =>
                s.Query(q => q
                    .GeoDistance(g => g
                        .Field(f => f.GeoLocation)
                        .Distance($"{request.Radius}km")
                        .Location(
                            GeoLocation.LatitudeLongitude(
                                new LatLonGeoLocation { Lat = 15.15, Lon = 20.20 }
                            )
                        )
                    )
                ), cancellationToken);

            return response.Documents;
        }
    }
}