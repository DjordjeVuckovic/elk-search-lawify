using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Lawify.Common.Errors;

public static class ErrorExtensions
{
    public static IResult ToErrorResult<T>(this Result<T,ErrorResult> errorResult)
    {
        var error = errorResult.Error;
        var statusCode = error.Error.StatusCode.HasValue ? error.Error.StatusCode.Value  : ErrorCode.Conflict;
        var description = error.Error.Description.GetValueOrDefault();
        var errorResponse = new ErrorResponse(error.Error.Error, statusCode, description, DateTime.UtcNow);
        return statusCode switch {
            ErrorCode.BadRequest => Results.BadRequest(errorResponse),
            ErrorCode.Unauthorized => Results.Unauthorized(),
            ErrorCode.Forbidden => Results.Forbid(),
            ErrorCode.NotFound => Results.NotFound(errorResponse),
            ErrorCode.Conflict => Results.Conflict(errorResponse),
            _ => throw new ArgumentOutOfRangeException(nameof(statusCode), statusCode, null)
        };
    }
}