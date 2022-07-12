using System.Linq;
using MassTransit;
using NUnit.Framework;
using MassTransit.Testing;
using System.Threading.Tasks;
using MatchMake.Backend.MessageBus.Consumers;
using MatchMake.Backend.MessageBus.Contracts;

namespace Consumers
{

    [TestFixture]
    public class HelloMessageConsumerShould
    {

        [Test]
        public async Task SuccessConsumeMessage()
        {

            var harness = new InMemoryTestHarness();
            var consumerHarness = harness.Consumer<HelloMessageConsumer>();

            await harness.Start();

            try
            {
                await harness.InputQueueSendEndpoint.Send(new HelloMessage());

                // did the endpoint consume the message
                Assert.IsTrue(harness.Consumed.Select<HelloMessage>().Any());

                // did the actual consumer consume the message
                Assert.IsTrue(consumerHarness.Consumed.Select<HelloMessage>().Any());
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}