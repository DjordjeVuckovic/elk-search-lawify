namespace Lawify.Search.Api.Features.SerbianSearch.Shared.Types;

public record Metadata(
    string? Title,
    string? FileName,
    DateTime CreatedAt,
    string? Author,
    string? Category
);