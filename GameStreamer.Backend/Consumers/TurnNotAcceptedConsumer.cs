using MassTransit;
using Newtonsoft.Json;
using GameStreamer.Backend.DTOs.MessageBus.Consume;

namespace GameStreamer.Backend.Consumers
{

    public class TurnNotAcceptedConsumer : IConsumer<TurnNotAcceptedDto>
    {
        public Task Consume(ConsumeContext<TurnNotAcceptedDto> context)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("!!! Turn wasn't correct!");
            
            var turnNotAcceptedDto = JsonConvert.SerializeObject(context.Message); ;
            Console.WriteLine($"TurnNotAcceptedConsumer consume message: {turnNotAcceptedDto}");
            
            Console.ResetColor();

            return Task.CompletedTask;
        }
    }
}