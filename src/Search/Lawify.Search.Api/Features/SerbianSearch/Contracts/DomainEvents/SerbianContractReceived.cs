using Lawify.Messaging.Events.Contracts;
using MediatR;

namespace Lawify.Search.Api.Features.SerbianSearch.Contracts.DomainEvents;

public record SerbianContractReceived : INotification
{
    public SerbianContractExported Contract { get; set; } = null!;
}