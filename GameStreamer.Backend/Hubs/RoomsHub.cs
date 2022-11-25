using GameStreamer.Backend.DTOs;
using GameStreamer.Backend.Interfaces;
using GameStreamer.Backend.Services;
using Microsoft.AspNetCore.SignalR;

namespace GameStreamer.Backend.Hubs
{
    public class RoomsHub : Hub<IRoomsHub>
    {

        private readonly IRoomsManager _roomsManager;

        public RoomsHub(IRoomsManager roomsManager)
        {
            _roomsManager = roomsManager;
        }

        public Task PlayerAddedLogin(string playerLogin)
        {
            _roomsManager.AddPlayerToServer(Context.ConnectionId, playerLogin);
            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var leavedPlayerData = _roomsManager.GetPlayerDataBy(Context.ConnectionId);

            Clients.All.PlayerLeavedServer(leavedPlayerData);
            
            _roomsManager.RemovePlayer();

            return base.OnDisconnectedAsync(exception);
        }

        public override Task OnConnectedAsync()
        {
            var playerForTransmitToClients = _roomsManager.GetRandomPlayer();
            Clients.All.NewPlayerJoined(playerForTransmitToClients);
            return base.OnConnectedAsync();
        }

        /// <summary>
        /// Метод добавляет клиентское соединение в группу
        /// </summary>
        /// <returns></returns>
        //public Task JoinRoom()
        //{
        //    return Groups.AddToGroupAsync(Context.ConnectionId, $"GameRoom-{Context.ConnectionId}");
        //}

        /// <summary>
        /// Метод удаляет клиентское соединение из группы
        /// </summary>
        /// <returns></returns>
        //public Task LeaveRoom()
        //{
        //    return Groups.RemoveFromGroupAsync(Context.ConnectionId, $"GameRoom-{Context.ConnectionId}");
        //}
    }
}