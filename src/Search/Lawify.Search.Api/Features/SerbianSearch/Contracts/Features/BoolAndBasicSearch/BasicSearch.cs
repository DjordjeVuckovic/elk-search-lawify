using CSharpFunctionalExtensions;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Search;
using Lawify.Search.Api.Features.SerbianSearch.Contracts.Factories;
using Lawify.Search.Api.Features.SerbianSearch.Contracts.Features.Dto;
using MediatR;
using Result = CSharpFunctionalExtensions.Result;

namespace Lawify.Search.Api.Features.SerbianSearch.Contracts.Features.BoolAndBasicSearch;

public static class BasicSearch
{
    public record Query(
        string Field,
        string Value,
        bool IsPhrase) : IRequest<Result<ContractSearchResponse>>;

    internal class Handler(ElasticsearchClient client): IRequestHandler<Query,Result<ContractSearchResponse>>
    {
        public async Task<Result<ContractSearchResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            if (request.IsPhrase) {
                var searchResponsePhrase = await client.SearchAsync<SerbianContractIndex>(s => s
                    .Query(q => q
                        .MatchPhrase(m => m
                            .Field(request.Field)
                            .Analyzer("serbian")
                            .Query(request.Value)))
                    .Highlight(h => h
                        .Fields(fields => fields
                            .Add(request.Field, new HighlightField
                            {
                                Type = HighlighterType.Plain,
                                NumberOfFragments = 100,
                                FragmentSize = 300
                            }))
                    ), cancellationToken);

                if (!searchResponsePhrase.IsValidResponse)
                    return Result.Failure<ContractSearchResponse>("Something went wrong with searching contracts");
                var hitsPhrase = searchResponsePhrase.Hits.ToList();
                var responsePhrase = ToSearchResponse(hitsPhrase,request);
                return responsePhrase;
            }
            var searchResponse = await client.SearchAsync<SerbianContractIndex>(s => s
                    .Query(q => q
                        .Match(m => m
                            .Field(request.Field)
                            .Analyzer("serbian")
                            .Query(request.Value)))
                    .Highlight(h => h
                        .Fields(fields => fields
                            .Add(request.Field, new HighlightField
                            {
                                Type = HighlighterType.Plain,
                                NumberOfFragments = 150,
                                FragmentSize = 300
                            }))
                    ), cancellationToken);
            if (!searchResponse.IsValidResponse)
                return Result.Failure<ContractSearchResponse>("Something went wrong with searching contracts");
            var hits = searchResponse.Hits.ToList();
            var response = ToSearchResponse(hits,request);
            return response;
        }

        private static ContractSearchResponse ToSearchResponse(List<Hit<SerbianContractIndex>> hits, Query request)
        {
            var hitResponses = new List<ContractHitResponse>();
            hits.ForEach(hit =>
            {
                if (hit.Highlight is not null && hit.Highlight.ContainsKey(request.Field))
                {
                    var highlightStrings = hit.Highlight[request.Field].ToList();
                    var highlight = "..." + string.Join(" ", highlightStrings) + "...";
                    var hitResponse = SerbianContractFactory.ToApi(hit.Source, highlight);
                    hitResponses.Add(hitResponse);
                }

                if (hit.Highlight is null || !hit.Highlight.ContainsKey(request.Field))
                {
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
}