using Lawify.ContentDispatchingProcessor.ProcessContent.ContentExtractors.Models;

namespace Lawify.ContentDispatchingProcessor.ProcessContent.ContentExtractors.Laws;

public record LawExtracted(string Content, ContentMetadata Metadata);