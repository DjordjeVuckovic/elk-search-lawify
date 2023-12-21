﻿using Carter;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using MediatR;

namespace Lawify.Search.Api.Features.SerbianSearch.Contracts.Feature;

public class ProcessContractContent
{
    public record Query(string Keyword) : IRequest<IReadOnlyCollection<SerbianContract>>;

    public class Handler(ElasticsearchClient elasticsearchClient) : IRequestHandler<Query, IReadOnlyCollection<SerbianContract>>
    {
        public async Task<IReadOnlyCollection<SerbianContract>> Handle(Query request, CancellationToken cancellationToken)
        {
            var response = await elasticsearchClient.SearchAsync<SerbianContract>(s => s
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
        app.MapGet("api/v1/serbian-contracts/{keyword}", async (string keyword, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new ProcessContractContent.Query(keyword), cancellationToken);
            return Results.Ok(result);
        });
    }
}