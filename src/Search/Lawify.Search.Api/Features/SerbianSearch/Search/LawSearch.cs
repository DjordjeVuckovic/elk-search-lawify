using Carter;
using Lawify.Common.Options;
using Microsoft.Extensions.Options;

namespace Lawify.Search.Api.Features.SerbianSearch.Search;

public static class LawSearch
{
}

public class LawSearchEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/search-laws", (ILogger<LawSearchEndpoint> logger, IOptions<ElkOptions> elkOptions) => {
            logger.LogInformation("ELK options: {@ElkOptions}", elkOptions);
            logger.LogError("Not working!");
            return Results.BadRequest();
        });
    }
}
