using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Search;
using MediatR;

namespace Lawify.Search.Api.Features.SerbianSearch.Contracts.Feature;

public class Highlighter
{
    public record Query<T>(
        string SearchTerm,
        string[] FieldsNameToHighlight,
        int FragmentSize,
        int NumberOfFragments
    ) : IRequest<IReadOnlyCollection<T>>;

    public class Handler<T> : IRequestHandler<Query<T>, IReadOnlyCollection<T>> where T : class
    {
        private readonly ElasticsearchClient _elasticClient;

        public Handler(ElasticsearchClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<IReadOnlyCollection<T>> Handle(Query<T> request, CancellationToken cancellationToken)
        {
            var response = await _elasticClient.SearchAsync<T>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(request.FieldsNameToHighlight.First())
                        .Query(request.SearchTerm)
                    )
                )
                .Highlight(h => h
                    .Fields(fields => request.FieldsNameToHighlight.Aggregate(
                        fields,
                        (current, fieldName) => current.Add(fieldName,
                            new HighlightField {
                                FragmentSize = request.FragmentSize,
                                NumberOfFragments = request.NumberOfFragments,
                            }))
                    )
                ), cancellationToken);

            return response.Documents;
        }
    }
}