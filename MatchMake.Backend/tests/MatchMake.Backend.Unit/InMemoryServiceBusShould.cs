using NUnit.Framework;
using MassTransit.Testing;
using System.Threading.Tasks;

namespace InMemoryServiceBusServer
{
    [TestFixture]
    public class InMemoryServiceBusShould
    {
        
        [Test]
        public async Task RunAndThenStopBus()
        {
            var harness = new InMemoryTestHarness();

            await harness.Start();
            
            await harness.Stop();
        }

    }
}
