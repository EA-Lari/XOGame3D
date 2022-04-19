using System.Diagnostics;
using MatchMake.Backend.Contracts;

namespace MatchMake.Backend.Processes
{
    public class HelloWorldTestProcess : IParallelProcess
    {

        private Timer _timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(HelloWorld, null, 0, 10000);
            return Task.CompletedTask;
        }

        private void HelloWorld(object state)
        {
            Debug.WriteLine("Hello World!");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
