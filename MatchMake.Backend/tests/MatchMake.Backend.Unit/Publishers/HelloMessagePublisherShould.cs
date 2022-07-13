using MassTransit;
using MatchMake.Backend.MessageBus.Contracts;
using MatchMake.Backend.MessageBus.Publishers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace MatchMake.Backend.Unit.Publishers
{
    
    [TestFixture]
    public class HelloMessagePublisherShould
    {

        [Test]
        [Ignore("Test is not complete")]
        public async Task PublishHelloMessage()
        {

            // Arrange
            var mockBus = new Mock<IBus>();

            IServiceCollection services = new ServiceCollection();
            
            services.AddSingleton(mockBus.Object);
            services.AddHostedService<HelloMessagePublisher>();
            
            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetService<IHostedService>() as HelloMessagePublisher;         

            // Act
            

            // Assert

        }
    }
}