using CSharpFunctionalExtensions;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Search;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Lawify.Search.Api.Features.SerbianSearch.Contracts.Factories;
using Lawify.Search.Api.Features.SerbianSearch.Contracts.Features.BoolAndBasicSearch.Models;
using Lawify.Search.Api.Features.SerbianSearch.Contracts.Features.Dto;
using MediatR;
using Result = CSharpFunctionalExtensions.Result;

namespace Lawify.Search.Api.Features.SerbianSearch.Contracts.Features.BoolAndBasicSearch;

public static class BoolSearch
{
    private const string Content = nameof(Content);
    private const string OrSplitOperator = " OR ";
    private const string AndSplitOperator = " AND ";
    private const string NotSplitOperator = " NOT ";
    private const string NotSplitOperatorOnEnd = "NOT ";
    private const string KayValueSpit = ":";

    public record Query(string BoolQuery) : IRequest<Result<ContractSearchResponse>>;

    internal class Handler(ElasticsearchClient client) : IRequestHandler<Query, Result<ContractSearchResponse>>
    {
        public async Task<Result<ContractSearchResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var parsedQuery = ParseQuery(request.BoolQuery);
            var condition = parsedQuery.GetExpressionType();
            var query = BuildQuery(condition);
            var hitsResponse = await client.SearchAsync<SerbianContractIndex>(s => s
                    .Query(query)
                    .Highlight(h => h
                        .Fields(f => f
                            .Add(Content, new HighlightField {
                                Type = HighlighterType.Plain,
                                NumberOfFragments = 80,
                                FragmentSize = 250
                            })
                        )),
                cancellationToken);
            if (!hitsResponse.IsValidResponse)
                return Result.Failure<ContractSearchResponse>("Elastic cannot apply bool search!");
            var hits = hitsResponse.Hits.ToList();
            var result = ToSearchResponse(hits, Content);
            return result;
        }

        private SearchExpression ParseQuery(string request)
        {
            if (request.Contains(AndSplitOperator))
                return ParseBoolQuery(AndSplitOperator, request.Split(AndSplitOperator));
            if (request.Contains(OrSplitOperator))
                return ParseBoolQuery(OrSplitOperator, request.Split(OrSplitOperator));
            if (request.Contains(NotSplitOperatorOnEnd))
                return ParseNotQuery(request[4..]);
            return ParseBasicSearch(request);
        }

        private SearchQuery BuildQuery(ISearchExpression expression)
        {
            if (expression is SearchLeaf simpleCondition) {
                return simpleCondition.IsPhrase
                    ? new MatchPhraseQuery(Field.KeyField) { Query = simpleCondition.Value, Field = simpleCondition.Field, Analyzer = "serbian" }
                    : new MatchQuery(Field.KeyField) { Query = simpleCondition.Value, Field = simpleCondition.Field, Analyzer = "serbian" };
            }

            if (expression is BoolQueryExpression booleanCondition) {
                var innerQueries = new List<Elastic.Clients.Elasticsearch.QueryDsl.Query>();
                booleanCondition.BoolQueryFields.ForEach(innerQuery => { innerQueries.Add(BuildQuery(innerQuery.GetExpressionType())!); });
                switch (booleanCondition.SplitOperator) {
                    case AndSplitOperator:
                        return new BoolQuery { Must = innerQueries, };
                    case OrSplitOperator:
                        return new BoolQuery { Should = innerQueries, MinimumShouldMatch = 1 };
                    case NotSplitOperator:
                        return new BoolQuery { MustNot = innerQueries };
                }
            }

            return new MatchQuery(Field.KeyField) { Query = "", Field = "", Analyzer = "serbian" };
        }

        private SearchExpression ParseNotQuery(string query)
        {
            var boolQuery = new BoolQueryExpression { SplitOperator = NotSplitOperator };
            var conditions = new List<SearchExpression> { ParseQuery(query) };
            boolQuery.BoolQueryFields = conditions;
            return new SearchExpression { BoolQueryExpression = boolQuery };
        }

        private SearchExpression ParseBasicSearch(string request)
        {
            var parts = request.Split(KayValueSpit);
            var searchLeaf = new SearchLeaf { Field = parts[0], Value = parts[1], IsPhrase = false };
            if (searchLeaf.Value.StartsWith("\"") && searchLeaf.Value.EndsWith("\"")) {
                searchLeaf.IsPhrase = true;
                searchLeaf.Value = searchLeaf.Value.Substring(1, searchLeaf.Value.Length - 1);
            }

            return new SearchExpression { SearchLeaf = searchLeaf };
        }

        private SearchExpression ParseBoolQuery(string splitOperator, IEnumerable<string> parts)
        {
            var boolQuery = new BoolQueryExpression { SplitOperator = splitOperator };
            var expressions = parts.Select(ParseQuery).ToList();

            boolQuery.BoolQueryFields = expressions;
            return new SearchExpression { BoolQueryExpression = boolQuery };
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