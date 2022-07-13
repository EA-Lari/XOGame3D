using System.Linq;
using MassTransit;
using NUnit.Framework;
using MassTransit.Testing;
using System.Threading.Tasks;
using MatchMake.Backend.MessageBus.Consumers;
using MatchMake.Backend.MessageBus.Contracts;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.Extensions.DependencyInjection;

namespace Consumers
{

    [TestFixture]
    public class HelloMessageConsumerShould
    {

        [Test]
        public async Task ConsumeHelloMessage()
        {

            // Arrange
            var harness = new InMemoryTestHarness();
            var mockLogger = new Mock<ILogger<HelloMessageConsumer>>();
            var consumerHarness = harness.Consumer(() => new HelloMessageConsumer(mockLogger.Object));

            await harness.Start();

            try
            {
                // Act
                await harness.InputQueueSendEndpoint.Send(new HelloMessage());


                // Assert

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

        [Test]
        public async Task PublishHelloMessageConsumedEvent()
        {

            // Arrange
            var harness = new InMemoryTestHarness();
            var mockLogger = new Mock<ILogger<HelloMessageConsumer>>();

            try
            {

                harness.Consumer(() => new HelloMessageConsumer(mockLogger.Object));

                await harness.Start();

                // Act
                await harness.InputQueueSendEndpoint.Send(new HelloMessage { Name = "SomeText"});

                //Assert
                // did the consumer publish the event
                Assert.IsTrue(harness.Published.Select<HelloMessageConsumedEvent>().Any());
            }
            finally
            {
                await harness.Stop();
            }
        }

    }
}