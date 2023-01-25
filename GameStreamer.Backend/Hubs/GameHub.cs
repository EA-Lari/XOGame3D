using GameStreamer.Backend.DTOs.GameClient;
using GameStreamer.Backend.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace GameStreamer.Backend.Hubs
{
    public class GameHub : Hub<IGameHub>
    {
        public GameHub()
        {

        }

        Task PlayerDoTurn(PlayerMadeTurnDto turnData)
        {
            Console.WriteLine(
                $"SignalR received player turn: cell: (X-{turnData.CellCoordinates.X}, Y-{turnData.CellCoordinates.Y}) area: (X-{turnData.SmallAreaCoordinates.X},Y-{turnData.SmallAreaCoordinates.Y})");
            return Task.CompletedTask;
        }

        public Task PlayerIsReady(bool isRandomMatch)
        {
            //_playerManager.MakePlayerReadyToGame(Context.ConnectionId);
            //_playerManager.SetMatchTypeToPlayer(Context.ConnectionId, isRandomMatch);

            // Добавить Publisher Endpoint для запуска проверки создания игры
            //_publishEndpoint.Publish<>();

            Clients.Caller.GameIsStarted();

            return Task.CompletedTask;
        }

    }
}
