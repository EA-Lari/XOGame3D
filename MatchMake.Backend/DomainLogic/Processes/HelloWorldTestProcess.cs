using MatchMake.Backend.Contracts;

namespace MatchMake.Backend.Processes
{
    public class HelloWorldTestProcess : IParallelProcess
    {

        //private Timer _timer;
        //private readonly ILogger _logger;

        public HelloWorldTestProcess()
        {
            //_logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //_timer = new Timer(HelloWorld, null, 0, 10000);
            //return Task.CompletedTask;
            HelloWorld();
            return Task.CompletedTask;
        }

        private void HelloWorld()
        {
            Console.WriteLine("HelloWorldProcess awake!");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            //_timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}