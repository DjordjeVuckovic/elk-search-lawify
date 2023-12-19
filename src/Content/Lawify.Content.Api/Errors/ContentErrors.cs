using CSharpFunctionalExtensions;
using Lawify.Common.Errors;

namespace Lawify.Content.Api.Errors;

public static class ContentErrors
{
    public static ErrorResult ContentNotFound(string message) => new(new ErrorObject(message, ErrorCode.NotFound, "Content not found"));
}