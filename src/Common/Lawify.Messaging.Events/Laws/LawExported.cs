namespace Lawify.Messaging.Events.Laws;

public record LawExported(string Content, LawExportedMetadata ExportedMetadata);

public record LawExportedMetadata(
    string Title,
    string FileName,
    DateTime CreatedAt,
    string? Author,
    string? Category
);