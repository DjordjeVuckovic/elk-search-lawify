using Lawify.Messaging.Events.Contents;
using MediatR;

namespace Lawify.ContentDispatchingProcessor.ProcessContent;

public record ContentReceived(
    string FileName,
    string BucketName,
    DocumentType DocumentType,
    string ContentType
) : INotification;