using MassTransit;
using GameStreamer.Backend.DTOs.MessageBus.Consume;

namespace GameStreamer.Backend.Consumers
{
    public class TurnAcceptedConsumer : IConsumer<TurnAcceptedDto>
    {
        public Task Consume(ConsumeContext<TurnAcceptedDto> context)
        {
            throw new NotImplementedException();
        }
    }
}
