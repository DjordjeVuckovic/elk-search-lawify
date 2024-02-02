namespace Lawify.Common.Errors;

public record ErrorResponse(string Message,ErrorCode Code, string? Description, DateTime Timestamp);