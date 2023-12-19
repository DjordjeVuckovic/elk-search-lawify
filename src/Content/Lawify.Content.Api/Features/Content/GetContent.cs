using Carter;
using CSharpFunctionalExtensions;
using Lawify.Common.Errors;
using Lawify.Common.Options;
using Lawify.Content.Api.Errors;
using Microsoft.Extensions.Options;

namespace Lawify.Content.Api.Features.Content;

public class GetContent
{

}
public class GetContentEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/search-laws", (ILogger<GetContentEndpoint> logger, IOptions<ElkOptions> elkOptions) => {
            logger.LogInformation("ELK options: {@ElkOptions}", elkOptions);
            logger.LogError("Not working!");
            var result = Result.Failure<GetContent,ErrorResult>(ContentErrors.ContentNotFound("Not found"));
            return result.ToErrorResult();
        });
    }
}