using Carter;
using Elastic.Clients.Elasticsearch;
using Lawify.Search.Api.Features.SerbianSearch.Contracts.Factories;
using Lawify.Search.Api.Features.SerbianSearch.Contracts.Features.Dto;
using MediatR;

namespace Lawify.Search.Api.Features.SerbianSearch.Contracts.Features.MatchAll;

public class MatchAllContracts
{
    public class Query : IRequest<List<ContractHitResponse>>;

    public class Handler(ElasticsearchClient elasticsearchClient) : IRequestHandler<Query, List<ContractHitResponse>>
    {
        public async Task<List<ContractHitResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var search =  await elasticsearchClient.SearchAsync<SerbianContractIndex>(cancellationToken);
            return search.Documents.Select(x => SerbianContractFactory.ToApi(x,string.Empty)).ToList();
        }
    }
}

public class MatchAllSerbianContractsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/serbian-contracts", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var laws = await sender.Send(new MatchAllContracts.Query(), cancellationToken);
            return Results.Ok(laws);
        });
    }
}