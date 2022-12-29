using MassTransit;
using Newtonsoft.Json;
using GameStreamer.Backend.DTOs.MessageBus.Consume;

namespace GameStreamer.Backend.Consumers
{



    public class TurnNotAcceptedConsumer : IConsumer<TurnNotAcceptedDto>
    {
        public Task Consume(ConsumeContext<TurnNotAcceptedDto> context)
        {
            var turnNotAcceptedDto = JsonConvert.SerializeObject(context.Message); ;
            Console.WriteLine($"TurnNotAcceptedConsumer consume message: {turnNotAcceptedDto}");

            return Task.CompletedTask;
        }
    }
}