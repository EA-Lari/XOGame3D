using MassTransit;

namespace GameStreamer.Backend.Consumers.Definitions
{
    public class TurnAcceptedConsumerDefinition : ConsumerDefinition<TurnAcceptedConsumer>
    {
        public TurnAcceptedConsumerDefinition()
        {

            EndpointName = "turn-accepted";

            ConcurrentMessageLimit = 2;
        }

        protected override void ConfigureConsumer(
            IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<TurnAcceptedConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Interval(5, 1000));
            endpointConfigurator.UseInMemoryOutbox();
        }
    }
}
