using Carter;
using Elastic.Clients.Elasticsearch;
using Lawify.Search.Api.Features.SerbianSearch.Laws.Dto;
using Lawify.Search.Api.Features.SerbianSearch.Shared.Shared;
using MediatR;

namespace Lawify.Search.Api.Features.SerbianSearch.Laws.Search;

public class SearchLawContentEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/serbian-laws/search", async (string content,ISender sender, CancellationToken cancellationToken) =>
        {
            var laws = await sender.Send(new SearchLawsContent.Query(content), cancellationToken);
            return Results.Ok(laws);
        });
    }
}

public static class SearchLawsContent
{
    private const string Content = "content";
    public record Query(string SearchQuery) : IRequest<List<LawHit>>;


    internal class Handler(ElasticsearchClient client, ISender sender,ILogger<Handler> logger) : IRequestHandler<Query, List<LawHit>>
    {
        public async Task<List<LawHit>> Handle(Query request, CancellationToken cancellationToken)
        {
            var searchResponse = await sender.Send(new Highlighter.Query<SerbianLawIndex>(
                request.SearchQuery,
                Content,
                [Content],
                80,
                250,
                new List<string> { "<em>" },
                new List<string> { "</em>" }
            ), cancellationToken);


            var response = CreateSearchResponse(searchResponse);
            return response;
        }

        private static List<LawHit> CreateSearchResponse(SearchResponse<SerbianLawIndex> searchResponse)
        {
            return searchResponse.Hits.Select(x => {
                var highlightStrings = x.Highlight[Content].ToList();
                var highlight = "..." + string.Join(" ", highlightStrings) + "...";
                var hitResponse = new LawHit(x.Source.Id,x.Source.Content, highlight, x.Source.FileName, x.Source.Metadata);
                return hitResponse;
            }).ToList();
        }
    }
}