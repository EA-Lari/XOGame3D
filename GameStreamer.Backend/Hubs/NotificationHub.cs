using System.Diagnostics;
using GameStreamer.Backend.Models;
using GameStreamer.Backend.Persistance.GameStreamerDbase.Entities;
using Microsoft.AspNetCore.SignalR;

namespace GameStreamer.Backend.Hubs
{
    public class NotificationHub : Hub<IGameClient>
    {

        public override Task OnConnectedAsync()
        {
            var newConnectMessage = NewPlayer.Create(Context.ConnectionId, string.Empty);

            var newConnectedPlayer = new ConnectedPlayerEntity
            {
                ConnectionId = Context.ConnectionId,
                ClientType = TypeOfConnectedClient.Unknown,
                RoomGuid = Guid.Empty,
                IsActive = true,
                NickName = "Anonimous"
            };

            //Clients.Others.Send(newConnectMessage);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {

            var newConnectedPlayer = new ConnectedPlayerEntity
            {
                ConnectionId = Context.ConnectionId,
                ClientType = TypeOfConnectedClient.Unknown,
                RoomGuid = Guid.Empty,
                IsActive = false,
                NickName = string.Empty
            };

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
