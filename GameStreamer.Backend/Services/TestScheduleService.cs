using GameStreamer.Backend.Hubs;
using Microsoft.AspNetCore.SignalR;
using GameStreamer.Backend.Interfaces;
using GameStreamer.Backend.DTOs;

namespace GameStreamer.Backend.Services
{
    public class TestScheduleService : BackgroundService
    {
        
        private readonly IHubContext<GameHub, IGameHub> _gameHub;
        private readonly IHubContext<RoomsHub, IRoomsHub> _roomsHub;
        private readonly Random _random = new Random();
        private readonly IRoomsManager _roomsManager;
        
        public TestScheduleService(IHubContext<GameHub, IGameHub> gameHub, IHubContext<RoomsHub, IRoomsHub> roomsHub, IRoomsManager roomsManager)
        {
            _gameHub = gameHub;
            _roomsHub = roomsHub;
            _roomsManager = roomsManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(20));

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                Console.WriteLine("TestScheduleService have got next tick! Waiting for 5 sec...");

                await _roomsHub.Clients.All.NewRoomAdded(
                    new GameRoomResponseDTO { 
                        RoomName = $"Room_{_random.Next(8000, 15000)}",
                        PlayersList = new List<PlayerDataResponseDTO> {
                            new PlayerDataResponseDTO { ConnectionId = "123", NickName = "Pidor_666" },
                            new PlayerDataResponseDTO { ConnectionId = "456", NickName = "Pidor_777" },
                        }
                    });

                //await _gameHub.Clients.All.TestBroadcastPublish("You are pidrila!");
                
            }
        }
    }
}
