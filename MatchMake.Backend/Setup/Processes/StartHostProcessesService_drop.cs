using Hangfire;
using MatchMake.Backend.Contracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MatchMake.Backend
{

    /// <summary>
    /// Служба для старта параллельных процессов внутри приложения
    /// </summary>
    public class StartHostProcessesService_drop : IHostedService
    {

        private readonly ILogger<StartHostProcessesService_drop> _logger;
        private readonly IEnumerable<IParallelProcess> _processesList;
        private readonly CancellationTokenSource _cts;

        public StartHostProcessesService_drop(ILogger<StartHostProcessesService_drop> logger, IEnumerable<IParallelProcess> processesList)
        {
            _logger = logger;
            _processesList = processesList;
            _cts = new CancellationTokenSource();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {

                foreach (var process in _processesList)
                {
                    Task.Run(() =>
                    {
                        try
                        {
                            process.StartAsync(_cts.Token);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, $"{GetType().Name}. Процесс: {process.GetType().Name}.");
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{GetType().Name}.");
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                foreach (var process in _processesList)
                {
                    Task.Run(() =>
                    {
                        try
                        {
                            process.StopAsync(_cts.Token);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, $"{GetType().Name}. Процесс: {process.GetType().Name}.");
                        }
                    });
                }

                Thread.Sleep(5000);

                _cts.Cancel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{GetType().Name}. Ошибка при остановке.");
            }

            return Task.CompletedTask;
        }
    }
}
