using Lawify.Search.Api.Features.SerbianSearch.Shared.Types;

namespace Lawify.Search.Api.Features.SerbianSearch.Laws.Dto;

public record LawHit(
    Guid Id,
    string Content,
    string? Highlight,
    string FileName,
    Metadata? Metadata
);