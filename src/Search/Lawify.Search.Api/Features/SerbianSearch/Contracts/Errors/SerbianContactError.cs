using Lawify.Common.Errors;

namespace Lawify.Search.Api.Features.SerbianSearch.Contracts.Errors;

public class SerbianContactError
{
    public static ErrorResult Conflict(string message, string description) => new(new ErrorObject(message, ErrorCode.NotFound, description));
}