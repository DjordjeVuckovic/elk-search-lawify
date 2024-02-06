using FluentResults;

namespace Lawify.Search.Api.LocationSearch.HttpHandler;

public interface IHttpClientHandler
{
    Task<Result<HttpResponseMessage>> SendAsync(HttpRequestMessage requestMessage, string? accessToken = null, string? tokenHeaderName = "Bearer");
}
