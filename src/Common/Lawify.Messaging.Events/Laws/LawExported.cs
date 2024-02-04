using Lawify.Messaging.Events.Contents;

namespace Lawify.Messaging.Events.Laws;

public record LawExported(string Content, ContentMetadataExported ExportedMetadataExported);