using Hangfire;
using MatchMake.Backend.Contracts;
using System.Text;

namespace MatchMake.Backend.Setup
{

    /// <summary>
    /// Класс собирает процессы в пачку и стартует Hangfire джобы
    /// </summary>
    public class ProcessStarter
    {
        // Принцип работы класса https://stackoverflow.com/questions/22384884/autofac-with-multiple-implementations-of-the-same-interface
        
        // Time to Cron Expression Online
        // https://www.freeformatter.com/cron-expression-generator-quartz.html

        private readonly IEnumerable<IParallelProcess> _processes;

        public ProcessStarter(IEnumerable<IParallelProcess> processes)
        {
            _processes = processes;
        }

        public void ScheduleAllProcesses()
        {

            var processNamesBuilder = new StringBuilder();

            foreach (var process in _processes)
            {

                RecurringJob.AddOrUpdate<HelloWorldTestProcess>("HelloWorldTestProcess", x => x.StartAsync(new CancellationToken()), process.SchedulingPeriod);
                RecurringJob.AddOrUpdate<NotificationProcess>("NotificationProcess", y => y.StartAsync(new CancellationToken()), "0/10 * * * * *");

                    var processName = process.GetType().Name;

                    RecurringJob.AddOrUpdate<IParallelProcess>(
                                    processName,
                                    x => x.StartAsync(new CancellationToken()),
                                    process.SchedulingPeriod
                                    );

                    processNamesBuilder.Append(processName + Environment.NewLine);

                
                
            }

            logger.LogInformation($"List of scheduling processes: {Environment.NewLine}{processNamesBuilder.ToString()}");

        }

    }
}