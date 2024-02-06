using Carter;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Search;
using Lawify.Search.Api.Features.SerbianSearch.Contracts.Factories;
using Lawify.Search.Api.Features.SerbianSearch.Contracts.Features.Dto;
using MediatR;

namespace Lawify.Search.Api.Features.SerbianSearch.Contracts.Features.Geospatial;

public class GeospatialSearchEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/serbian-contracts/geospatial-search", async (
            double lat, double lon, string radiusM, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GeospatialSearch.Query(lat, lon, radiusM), cancellationToken);
            return Results.Ok(result.Hits);
        });
    }
}

public class GeospatialSearch
{
    private const string Content = nameof(Content);
    public record Query(
        double Lat,
        double Long,
        string RadiusMeters
    ) : IRequest<ContractSearchResponse>;

    public class Handler(ElasticsearchClient elasticsearchClient)
        : IRequestHandler<Query, ContractSearchResponse>
    {
        public async Task<ContractSearchResponse> Handle(
            Query request,
            CancellationToken cancellationToken
        )
        {
            var latLongLocation = new LatLonGeoLocation
            {
                Lat = request.Lat,
                Lon = request.Long
            };
            var geoLocation =  GeoLocation.LatitudeLongitude(latLongLocation);
            var response = await elasticsearchClient.SearchAsync<SerbianContractIndex>(s =>
                s.Query(q => q
                    .GeoDistance(g => g
                        .Field(f => f.GeoLocation)
                        .Distance($"{request.RadiusMeters}m")
                        .Location(geoLocation)
                    )
                ), cancellationToken);

            return ToSearchResponse(response.Hits.ToList(),Content);
        }
    }

    public static ContractSearchResponse ToSearchResponse(List<Hit<SerbianContractIndex>> hits, string hitHighlightKey)
    {
        var hitResponses = new List<ContractHitResponse>();
        hits.ForEach(hit => {
            if (hit.Highlight is not null && hit.Highlight.ContainsKey(hitHighlightKey)) {
                var highlightStrings = hit.Highlight[hitHighlightKey].ToList();
                var highlight = "..." + string.Join(" ", highlightStrings) + "...";
                var hitResponse = SerbianContractFactory.ToApi(hit.Source, highlight);
                hitResponses.Add(hitResponse);
            }

            if (hit.Highlight is null || !hit.Highlight.ContainsKey(hitHighlightKey)) {
                var highlight = hit.Source!.Content.Length > 150
                    ? hit.Source.Content[..150] + "..."
                    : hit.Source.Content;
                var hitResponse = SerbianContractFactory.ToApi(hit.Source, highlight);
                hitResponses.Add(hitResponse);
            }
        });
        return new ContractSearchResponse(hitResponses, hits.Count);
    }
}