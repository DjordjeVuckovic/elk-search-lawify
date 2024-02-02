using Lawify.Messaging.Events.Laws;
using MassTransit;

namespace Lawify.Search.Api.MessageBroker.Consumers;

public class LawExportedConsumer : IConsumer<LawExported>
{
    public Task Consume(ConsumeContext<LawExported> context)
    {
        throw new NotImplementedException();
    }
}