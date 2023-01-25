using MassTransit;

namespace GameStreamer.Backend.Consumers.Definitions
{
    public class TurnNotAcceptedConsumerDefinition : ConsumerDefinition<TurnDeniedConsumer>
    {
        public TurnNotAcceptedConsumerDefinition()
        {

            EndpointName = "turn-not-accepted";

            ConcurrentMessageLimit = 2;
        }

        protected override void ConfigureConsumer(
            IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<TurnDeniedConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Interval(5, 1000));
            endpointConfigurator.UseInMemoryOutbox();
        }

    }
}
