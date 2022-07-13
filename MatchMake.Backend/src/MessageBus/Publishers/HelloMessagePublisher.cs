using MassTransit;
using MatchMake.Backend.MessageBus.Contracts;
using Microsoft.Extensions.Hosting;

namespace MatchMake.Backend.MessageBus.Publishers
{

    public class HelloMessagePublisher : BackgroundService
    {

        private readonly IBus _bus;

        public HelloMessagePublisher(IBus bus)
        {
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _bus.Publish(new HelloMessage
                {
                    Name = "Pedik!"
                }, stoppingToken);
                
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}