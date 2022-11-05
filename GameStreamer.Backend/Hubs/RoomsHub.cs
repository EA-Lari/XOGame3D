using Microsoft.AspNetCore.SignalR;

namespace GameStreamer.Backend.Hubs
{
    public class RoomsHub : Hub
    {
        /// <summary>
        /// Метод потоковой отправки текущей даты клиентам
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<DateTime> StreamCurrentDate(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                yield return DateTime.UtcNow;
                await Task.Delay(1000, cancellationToken);
            }
        }

        /// <summary>
        /// Тестовый Echo метод
        /// </summary>
        /// <param name="nickName"></param>
        /// <returns></returns>
        public async Task SendHelloWorld(string nickName)
        {
            await Clients.All.SendAsync($"Hello from SignalR, {nickName}!");
        }
    }
}