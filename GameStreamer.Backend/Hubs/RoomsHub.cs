using Microsoft.AspNetCore.SignalR;

namespace GameStreamer.Backend.Hubs
{
    public class RoomsHub : Hub
    {
        public async IAsyncEnumerable<DateTime> Streaming(CancellationToken cancellationToken)
        {
            while (true)
            {
                yield return DateTime.UtcNow;
                await Task.Delay(3000, cancellationToken);
            }
        }

        public async Task SendHelloWorld(string nickName)
        {
            await Clients.All.SendAsync($"Hello from SignalR, {nickName}!");
        }
    }
}