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

        public Task PlayerChangedLogin(string prevNickName, string playerLogin)
        {
            var changedPlayerDataDto = _playerManager.ChangePlayerNickName(prevNickName, playerLogin);
            Clients.All.PlayerChangedNickName(changedPlayerDataDto);
            return Task.CompletedTask;
        }

        public Task PlayerChoseRandomGame()
        {
            return Task.CompletedTask;
        }

        public Task PlayerChoseStandardGame() => Task.CompletedTask;

        public Task GreetNewPlayer(string nickName)
        {
            var newPlayerDto = _playerManager.AddNewPlayerToServer(nickName);

            //var gameRoomsList = _roomManager.GetAllGameRooms();

            //var playersWithoutRoomList = _playerManager.GetAllPlayersWithoutRoom();

            Clients.AllExcept(Context.ConnectionId).NewPlayerJoined(newPlayerDto);
            //Clients.Caller.UpdatePlayersWithoutRooms(playersWithoutRoomList);

            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {

            var removedPlayerDto = _playerManager.RemovePlayer(Context.ConnectionId);

            Clients.AllExcept(Context.ConnectionId).PlayerLeavedServer(removedPlayerDto);

            return base.OnDisconnectedAsync(exception);
        }

    }
}