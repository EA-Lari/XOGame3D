using MassTransit;
using Microsoft.Extensions.Logging;
using MatchMake.Backend.MessageBus.Contracts;

namespace MatchMake.Backend.MessageBus.Consumers
{

    /// <summary>
    /// Класс-обработчик сообщения из очереди по типу Контроллера в MVC
    /// </summary>
    public class HelloMessageConsumer : IConsumer<HelloMessage>
    {

        private readonly ILogger<HelloMessageConsumer> _logger;
        
        public HelloMessageConsumer(ILogger<HelloMessageConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<HelloMessage> context)
        {
            _logger.LogInformation("Hello {Name}", context.Message.Name);
            
            return Task.CompletedTask;
        }
    }
}