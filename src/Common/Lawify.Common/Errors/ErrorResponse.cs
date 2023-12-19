namespace Lawify.Common.Errors;

public record ErrorResponse(string Error,ErrorCode Code, string? Description, DateTime Timestamp);