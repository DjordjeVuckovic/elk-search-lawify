using Lawify.Search.Api.Features.SerbianSearch.Shared.Types;

namespace Lawify.Search.Api.Features.SerbianSearch.Contracts.Types;

public record Government(string Name,string AdministrationLevel,string Phone, string Email, Address? Address);