using MediatR;

namespace Lawify.Search.Api.Features.SerbianSearch.Laws.DomainEvents;

public record LawReceived(
    string Content,
    LawReceivedMetadata Metadata
) : INotification;

public record LawReceivedMetadata(
    string? Title,
    string? FileName,
    DateTime CreatedAt,
    string? Author,
    string? Category
);