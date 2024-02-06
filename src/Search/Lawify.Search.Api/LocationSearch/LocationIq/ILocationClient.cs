using FluentResults;

namespace Lawify.Search.Api.LocationSearch.LocationIq;

public interface ILocationClient
{
    Task<Result<Location>> GetAddressLocation(Address address);
}
