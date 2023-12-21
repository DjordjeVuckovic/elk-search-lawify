using Lawify.Search.Api.Features.SerbianSearch.Shared.Types;

namespace Lawify.Search.Api.Features.SerbianSearch.Contracts.Types;

public record Agency(string Name,string Phone, string Email, Address? Address);