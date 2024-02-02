namespace Lawify.Messaging.Events.Contents;

public record UploadedContent(
    string FileName,
    string BucketName,
    DocumentType DocumentType,
    string ContentType
);