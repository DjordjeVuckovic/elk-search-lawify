namespace Lawify.Search.Api.Features.SerbianSearch.Shared.Dto;

public record MetadataDto(
    string? Title,
    string? FileName,
    DateTime CreatedAt,
    string? Author,
    string? Category
);