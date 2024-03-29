﻿using Carter;
using Elastic.Clients.Elasticsearch;
using MediatR;

namespace Lawify.Search.Api.Features.SerbianSearch.Contracts.Features.BoolAndBasicSearch;

public class SerbianContractContentSearch
{
    public record Query(string Keyword) : IRequest<IReadOnlyCollection<SerbianContractIndex>>;

    public class Handler(ElasticsearchClient elasticsearchClient) : IRequestHandler<Query, IReadOnlyCollection<SerbianContractIndex>>
    {
        public async Task<IReadOnlyCollection<SerbianContractIndex>> Handle(Query request, CancellationToken cancellationToken)
        {
            var response = await elasticsearchClient.SearchAsync<SerbianContractIndex>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Content)
                        .Query(request.Keyword)
                        .Analyzer("serbian")
                    )
                ), cancellationToken);

            return response.Documents;
        }
    }
}

public class CreateProcessContractContentApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/serbian-contracts/content/{keyword}", async (string keyword, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new SerbianContractContentSearch.Query(keyword), cancellationToken);
            return Results.Ok(result);
        });
    }
}