using GameStreamer.Backend.Models;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace GameStreamer.Backend.Hubs
{
    public class NotificationHub : Hub<IGameClient>
    {

        public override Task OnConnectedAsync()
        {
            var newConnectMessage = NewPlayer.Create(Context.ConnectionId, string.Empty);

            //Clients.Others.Send(newConnectMessage);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            //var disconnectMessage = new NewMessage
            //{
            //    Sender = "Anonymous",
            //    Text = $"client disconnected: {Context.ConnectionId}"
            //};

            //Clients.Others.Send(disconnectMessage);

            return base.OnDisconnectedAsync(exception);
        }

        protected override void Dispose(bool disposing)
        {
            Debug.WriteLine("Hub Disposed");
            base.Dispose(disposing);
        }
    }
}
