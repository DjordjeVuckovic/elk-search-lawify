using Carter;
using CSharpFunctionalExtensions;
using Elastic.Clients.Elasticsearch;
using Lawify.Common.Errors;
using Lawify.Search.Api.Features.SerbianSearch.Contracts.Errors;
using MediatR;
using Result = CSharpFunctionalExtensions.Result;

namespace Lawify.Search.Api.Features.SerbianSearch.Contracts.Feature;

public class CreateContract
{
    public record Command(string Content) : IRequest<Result<Response,ErrorResult>>;
    public record Response(string Id);

    public class Handler(ElasticsearchClient elasticsearchClient) : IRequestHandler<Command, Result<Response,ErrorResult>>
    {
        public async Task<Result<Response,ErrorResult>> Handle(Command request, CancellationToken cancellationToken)
        {
            var contract = new SerbianContract
            {
                Id = SerbianContract.CreateId(),
                Content = request.Content
            };

            var response = await elasticsearchClient.IndexAsync(contract, cancellationToken);
            return response.IsValidResponse
                ? Result.Success<Response,ErrorResult>(new Response(response.Id))
                : Result.Failure<Response,ErrorResult>(SerbianContactError.Conflict("Failed to create contract", response.DebugInformation));
        }
    }
}

public class CreateCreateContractApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/serbian-contracts", async (CreateContract.Command command,ISender sender, CancellationToken cancellationToken) =>
        {
           var result = await sender.Send(command, cancellationToken);
           return result.IsSuccess ? Results.Ok(result.Value) : Results.Conflict(result.Error);
        });
    }
}