using Hangfire;
using System.Text;
using MatchMake.Backend.Contracts;
using Microsoft.Extensions.Logging;

namespace MatchMake.Backend.Setup
{

    /// <summary>
    /// Класс собирает процессы в пачку и стартует Hangfire джобы
    /// </summary>
    public class ProcessStarter : IProcessStarter
    {
        // Принцип работы класса https://stackoverflow.com/questions/22384884/autofac-with-multiple-implementations-of-the-same-interface
        
        // Time to Cron Expression Online
        // https://www.freeformatter.com/cron-expression-generator-quartz.html

        private readonly IEnumerable<IParallelProcess> _processes;
        private readonly ILogger<ProcessStarter> _logger;

        public ProcessStarter(IEnumerable<IParallelProcess> processes, ILogger<ProcessStarter> logger)
        {
            _processes = processes;
            _logger = logger;
        }

        public void ScheduleAllProcesses()
        {

            var processNamesBuilder = new StringBuilder();

            foreach (var process in _processes)
            {

                RecurringJob.AddOrUpdate(() => process.StartAsync(new CancellationToken()), process.SchedulingPeriod);
                
                processNamesBuilder.Append(process.GetType().Name + Environment.NewLine);
                
            }
            
            // CronConverters https://code.4noobz.net/hangfire-simple-cron-expression-converter/
            _logger.LogInformation($"List of scheduling processes: {Environment.NewLine}{processNamesBuilder.ToString()}");

        }

    }
}