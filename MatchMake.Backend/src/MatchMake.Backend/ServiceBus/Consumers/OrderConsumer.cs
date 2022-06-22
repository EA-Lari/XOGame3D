using MassTransit;
using Contracts.ServiceBus.Models;

namespace MatchMake.Backend.ServiceBus.Consumers
{
    public class OrderConsumer : IConsumer<Order>
    {
        private readonly ILogger<OrderConsumer> _logger;

        public OrderConsumer(ILogger<OrderConsumer> logger)
        {
            this._logger = logger;
        }

        public async Task Consume(ConsumeContext<Order> context)
        {
            await Console.Out.WriteLineAsync(context.Message.ShortDescription);
            this._logger.LogInformation($"Got new order {context.Message.ShortDescription}");
        }
    }
}
