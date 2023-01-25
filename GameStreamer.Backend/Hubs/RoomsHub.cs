using MassTransit;
using Microsoft.AspNetCore.SignalR;
using GameStreamer.Backend.Services;
using GameStreamer.Backend.Interfaces;

namespace GameStreamer.Backend.Hubs
{
    public class RoomsHub : Hub<IRoomsHub>
    {

        private readonly IRoomManager _roomManager;
        private readonly IPlayerManager _playerManager;
        private readonly IPublishEndpoint _publishEndpoint;

        public RoomsHub(IRoomManager roomManager, IPlayerManager playerManager)
        {
            _roomManager = roomManager;
            _playerManager = playerManager;
        }

        public Task PlayerAddedLogin(string playerLogin)
        {
            var changedPlayerDataDto = _playerManager.ChangePlayerNickName(Context.ConnectionId, playerLogin);
            Clients.All.PlayerChangedNickName(changedPlayerDataDto);
            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {

            var removedPlayerDto = _playerManager.RemovePlayer(Context.ConnectionId);

            Clients.AllExcept(Context.ConnectionId).PlayerLeavedServer(removedPlayerDto);

            return base.OnDisconnectedAsync(exception);
        }

        public override Task OnConnectedAsync()
        {
            var newPlayerDto = _playerManager.AddPlayerToServer(Context.ConnectionId, null);

            var gameRoomsList = _roomManager.GetAllGameRooms();
            var playersWithoutRoomList = _playerManager.GetAllPlayersWithoutRoom();

            Clients.AllExcept(Context.ConnectionId).NewPlayerJoined(newPlayerDto);
            Clients.Caller.UpdatePlayersWithoutRooms(playersWithoutRoomList);
            return base.OnConnectedAsync();
        }

    }
}