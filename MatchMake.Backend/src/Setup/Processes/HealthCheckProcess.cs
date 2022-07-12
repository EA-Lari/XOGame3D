using MatchMake.Backend.Contracts;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;

namespace MatchMake.Backend.Processes.Startup
{
    public class HealthCheckProcess : IParallelProcess
    {

        private const int port = 3000;

        private readonly ILogger _logger;
        private readonly TcpListener _tcpListener;

        public HealthCheckProcess(ILogger logger)
        {
            _logger = logger;
            _tcpListener = new TcpListener(IPAddress.Any, port);
        }

        public string SchedulingPeriod => "0/15 * * * * *";

        public string ResolveKey => this.GetType().Name;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _tcpListener.Start();

            // Принимаем клиентов в бесконечном цикле
            while (true)
            {
                // Получаем входящее соединение
                using (var tcpClient = _tcpListener.AcceptTcpClient())
                {
                    //_logger.Trace("Я жив и работаю!");

                    // Закрываем соединение
                    tcpClient.Close();
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
                _tcpListener?.Stop();

                return Task.CompletedTask;
            }
    }
}
