using Microsoft.Extensions.Hosting;

namespace MatchMake.Backend.Contracts
{

    /// <summary>
    /// Контракт для реализации параллельных во времени процессов в абстрактном приложении
    /// </summary>
    public interface IParallelProcess : IHostedService
    {

        public string SchedulingPeriod { get; }
        public string ResolveKey { get; }

    }

}