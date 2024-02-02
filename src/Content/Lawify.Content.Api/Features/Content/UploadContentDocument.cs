using CSharpFunctionalExtensions;
using FluentValidation;
using Lawify.Messaging.Events.Contents;
using MassTransit;
using MediatR;
using Minio;
using Minio.DataModel.Args;

namespace Lawify.Content.Api.Features.Content;

public class UploadContentDocument
{
    public record Command(IFormFile File, string BucketName, DocumentType DocumentType) : IRequest<Result>;

    internal class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.File)
                .NotNull()
                .WithMessage("File is required");

            RuleFor(x => x.File.FileName)
                .NotNull()
                .NotEmpty()
                .WithMessage("File Name is required");
        }
    }

    internal class Handler(
        IMinioClient minioClient,
        ILogger<Handler> logger,
        Validator validator,
        IPublishEndpoint eventBus) : IRequestHandler<Command, Result>
    {
        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var validateAsync = await validator.ValidateAsync(request, cancellationToken);
            if (!validateAsync.IsValid) {
                return Result.Failure(validateAsync.Errors.First().ErrorMessage);
            }

            var putArgs = new PutObjectArgs()
                .WithObject(request.File.FileName)
                .WithObjectSize(request.File.Length)
                .WithStreamData(request.File.OpenReadStream())
                .WithContentType(request.File.ContentType)
                .WithBucket(request.BucketName);

            try {
                await minioClient
                    .PutObjectAsync(
                        putArgs,
                        cancellationToken
                    ).ConfigureAwait(false);

                await PublishEvent(request, cancellationToken);

                return Result.Success();
            } catch (Exception e) {
                logger.LogError("Error uploading file: {Message}", e.Message);
                return Result.Failure("Error uploading file");
            }
        }

        private async Task PublishEvent(Command request, CancellationToken cancellationToken)
        {
            await eventBus.Publish(
                new UploadedContent(
                    request.File.FileName,
                    request.BucketName,
                    request.DocumentType,
                    request.File.ContentType
                ),
                cancellationToken
            );
        }
    }
}