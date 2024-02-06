using Carter;
using CSharpFunctionalExtensions;
using Lawify.Content.Api.Configuration;
using Lawify.Messaging.Events.Contents;
using MediatR;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace Lawify.Content.Api.Features.Content;

public class DownloadContentDocument
{
    public record Query(DocumentType Type,
        string FileName
    ) : IRequest<Result<DocumentResponse>>;
    internal class Handler(IMinioClient minioClient,
        ILogger<Handler> logger,
        IOptions<MinioOptions> minioOptions
    ): IRequestHandler<Query,Result<DocumentResponse>>
    {
        public async Task<Result<DocumentResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            switch (request.Type)
            {
                case DocumentType.Contract:
                    return  await GetDocumentFromDb(request.FileName, minioOptions.Value.ContractsBucketName!,cancellationToken);
                default:
                    return await GetDocumentFromDb(request.FileName, minioOptions.Value.LawsBucketName!,cancellationToken);
            }
        }

        private async Task<DocumentResponse> GetDocumentFromDb(string fileName, string bucketName,
            CancellationToken cancellationToken)
        {
            var fileStream = new MemoryStream();
            var getObject = new GetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileName)
                .WithCallbackStream(stream => { stream.CopyTo(fileStream); });

            try
            {
                await minioClient.GetObjectAsync(getObject, cancellationToken);
            }
            catch (MinioException e)
            {
                logger.LogError("Error getting file: {@Message}", e.Message);
            }

            var response = new DocumentResponse
            {
                DocumentStream = fileStream,
                FileName = fileName
            };
            return response;
        }
    }
}

public class DownloadDocumentEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/contents/{fileName}/{type}", async (string fileName, DocumentType type, ISender sender) =>
        {
            var result = await sender.Send(new DownloadContentDocument.Query(type, fileName));
            if (result.IsSuccess)
                result.Value.DocumentStream.Position = 0;
            return result.IsSuccess
                ? Results.File(result.Value.DocumentStream, "application/pdf", fileName)
                : Results.BadRequest();
        });
    }
}