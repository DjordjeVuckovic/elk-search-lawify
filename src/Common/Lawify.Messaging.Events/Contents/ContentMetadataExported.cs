namespace Lawify.Messaging.Events.Contents;

public record ContentMetadataExported(
    string? Title,
    string? FileName,
    DateTime CreatedAt,
    string? Author,
    string? Category
);