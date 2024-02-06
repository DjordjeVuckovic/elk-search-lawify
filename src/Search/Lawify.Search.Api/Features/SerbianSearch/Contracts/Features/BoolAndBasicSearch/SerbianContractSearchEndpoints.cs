using Carter;
using MediatR;

namespace Lawify.Search.Api.Features.SerbianSearch.Contracts.Features.BoolAndBasicSearch;

public class SerbianContractSearchEndpoints : ICarterModule
{
    private const string BaseUrl = "api/v1/serbian-contracts";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet($"{BaseUrl}/basic-search",
            async (string field, string value, bool isPhrase, ISender sender) =>
            {
                var result = await sender.Send(new BasicSearch.Query(
                    field,
                    value,
                    isPhrase
                ));
                return result.IsSuccess
                    ? Results.Ok(result.Value.Hits)
                    : Results.Conflict(result.Error);

            }
        );
        app.MapGet($"{BaseUrl}/bool-search",async (string query, ISender sender) =>
        {
           var result = await sender.Send(new BoolSearch.Query(query));
           return result.IsSuccess
               ? Results.Ok(result.Value.Hits)
               : Results.Conflict(result.Error);
        });
    }
}