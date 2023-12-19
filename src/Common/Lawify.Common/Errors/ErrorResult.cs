using CSharpFunctionalExtensions;

namespace Lawify.Common.Errors;
public record ErrorObject(string Error,Maybe<ErrorCode> StatusCode, Maybe<string> Description);
public enum ErrorCode
{
    BadRequest = 400,
    Unauthorized = 401,
    Forbidden = 403,
    NotFound = 404,
    Conflict = 409
}

public record ErrorResult(ErrorObject Error) : IError<ErrorObject>;