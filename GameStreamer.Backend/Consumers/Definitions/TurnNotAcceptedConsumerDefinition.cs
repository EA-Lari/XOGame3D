using MassTransit;

namespace GameStreamer.Backend.Consumers.Definitions
{
    public class TurnNotAcceptedConsumerDefinition : ConsumerDefinition<TurnNotAcceptedConsumer>
    {
        public TurnNotAcceptedConsumerDefinition()
        {

            EndpointName = "turn-not-accepted";

            ConcurrentMessageLimit = 2;
        }

        protected override void ConfigureConsumer(
            IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<TurnNotAcceptedConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Interval(5, 1000));
            endpointConfigurator.UseInMemoryOutbox();
        }

    }
}
