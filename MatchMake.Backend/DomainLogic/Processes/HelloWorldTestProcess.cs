using Hangfire;
using MatchMake.Backend.Contracts;

namespace MatchMake.Backend.Processes
{
    public class HelloWorldTestProcess : IParallelProcess
    {

        private string everySecondCron = "0/5 * * * * *";

        public string SchedulingPeriod { get => everySecondCron; }

        public string ResolveKey => this.GetType().Name;

        [DisableConcurrentExecution(10)]
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