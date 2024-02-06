namespace Lawify.Content.Api.Features.Content;

public class DocumentResponse
{
    public Stream DocumentStream { get; set; } = null!;
    public string FileName { get; set; } = null!;
}