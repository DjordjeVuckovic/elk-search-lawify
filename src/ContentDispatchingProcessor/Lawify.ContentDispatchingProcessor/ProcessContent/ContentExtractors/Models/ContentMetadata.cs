namespace Lawify.ContentDispatchingProcessor.ProcessContent.ContentExtractors.Models;

public record ContentMetadata(
    string Title,
    string FileName,
    DateTime CreatedAt,
    string? Author,
    string? Category
);