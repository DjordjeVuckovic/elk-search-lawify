using Carter;
using Lawify.Content.Api.Configuration;
using Lawify.Content.Api.Features.Content;
using Lawify.Messaging.Events.Contents;
using MediatR;
using Microsoft.Extensions.Options;

namespace Lawify.Content.Api.Features.Laws.Upload;

public class UploadLawDocumentEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/laws/upload",
            async (IFormFile file, ISender sender, IOptions<MinioOptions> options) => {
                var minioOptions = options.Value;
                var result = await sender.Send(
                    new UploadContentDocument.Command(
                        file,
                        minioOptions.LawsBucketName!,
                        DocumentType.Law
                        )
                );

                return result.IsSuccess
                    ? Results.Ok()
                    : Results.Conflict(new { error = result.Error });
            }).DisableAntiforgery();
    }
}