using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Search;
using MediatR;

namespace Lawify.Search.Api.Features.SerbianSearch.Shared.Shared;

public class Highlighter
{
    public record Query<T>(
        string SearchQuery,
        string MatchFieldName,
        string[] FieldsNameToHighlight,
        int FragmentSize,
        int NumberOfFragments,
        List<string>? PreTags,
        List<string>? PostTags
    ) : IRequest<SearchResponse<T>>;

    public class Handler<T> : IRequestHandler<Query<T>, SearchResponse<T>> where T : class
    {
        private readonly ElasticsearchClient _elasticClient;
        private readonly ILogger<Handler<T>> _logger;

        public Handler(ElasticsearchClient elasticClient, ILogger<Handler<T>> logger)
        {
            _elasticClient = elasticClient;
            _logger = logger;
        }

        public async Task<SearchResponse<T>> Handle(Query<T> request, CancellationToken cancellationToken)
        {
            var response = await _elasticClient.SearchAsync<T>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(request.MatchFieldName)
                        .Query(request.SearchQuery)
                        .Analyzer("serbian")
                    )
                )
                .Highlight(h => h
                    .Fields(fields => request.FieldsNameToHighlight.Aggregate(
                        fields,
                        (current, fieldName) => current.Add(fieldName,
                            new HighlightField {
                                FragmentSize = request.FragmentSize,
                                NumberOfFragments = request.NumberOfFragments,
                                PreTags = request.PreTags ?? ["<em>"],
                                PostTags = request.PostTags ?? ["</em>"],
                                Type = HighlighterType.Plain
                            }))
                    )
                ), cancellationToken);

            if (!response.IsValidResponse) {
                _logger.LogError("Something have gone wrong in content search of laws!");
            }

            return response;
        }
    }
}