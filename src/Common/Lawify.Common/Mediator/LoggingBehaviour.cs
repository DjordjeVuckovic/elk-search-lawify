using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Lawify.Common.Mediator;

public class LoggingBehaviour<TRequest,TResponse> : IPipelineBehavior<TRequest,TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResult, new()
{
    private readonly ILogger<LoggingBehaviour<TRequest,TResponse>> _logger;

    public LoggingBehaviour(
        ILogger<LoggingBehaviour<TRequest,TResponse>> logger)
    {
        _logger = logger;
    }
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        _logger.LogInformation("Starting request: {@Name}, {@DateTimeUtc}",
            requestName,
            DateTime.UtcNow);
        var result = await next();
        if (result.IsFailure)
        {
            _logger.LogError("Request failure: {@Name}, {@userEmail}",
                requestName,
                DateTime.UtcNow);
        }
        _logger.LogInformation("Completed request: {@Name}, {@DateTimeUtc}",
            requestName,
            DateTime.UtcNow);
        return result;
    }
}
