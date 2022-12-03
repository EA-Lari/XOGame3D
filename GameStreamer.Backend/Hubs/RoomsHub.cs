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
            _roomsManager.ChangePlayerNickName(Context.ConnectionId, playerLogin);
            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {

            var removedPlayerDto = _roomsManager.RemovePlayer(Context.ConnectionId);

            Clients.AllExcept(Context.ConnectionId).PlayerLeavedServer(removedPlayerDto);

            return base.OnDisconnectedAsync(exception);
        }

        public override Task OnConnectedAsync()
        {
            var newPlayerDto = _roomsManager.AddPlayerToServer(Context.ConnectionId, null);

            var gameRoomsList = _roomsManager.GetAllGameRooms();
            var playersWithoutRoomList = _roomsManager.GetAllPlayersWithoutRoom();

            Clients.AllExcept(Context.ConnectionId).NewPlayerJoined(newPlayerDto);
            Clients.Caller.UpdatePlayersWithoutRooms(playersWithoutRoomList);
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