using Carter;
using Elastic.Clients.Elasticsearch;
using MediatR;

namespace Lawify.Search.Api.Features.SerbianSearch.Laws.Search;

public class MatchAllSerbianLawsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/serbian-laws", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var laws = await sender.Send(new MatchAllLaws.Query(), cancellationToken);
            return Results.Ok(laws);
        });
    }
}

public class MatchAllLaws
{
    public class Query : IRequest<List<SerbianLawIndex>>;

    public class Handler(ElasticsearchClient elasticsearchClient) : IRequestHandler<Query, List<SerbianLawIndex>>
    {
        public async Task<List<SerbianLawIndex>> Handle(Query request, CancellationToken cancellationToken)
        {
           var search =  await elasticsearchClient.SearchAsync<SerbianLawIndex>(cancellationToken);
           return search.Documents.ToList();
        }
    }
}