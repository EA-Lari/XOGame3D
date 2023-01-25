using GameStreamer.Backend.Hubs;
using Microsoft.AspNetCore.SignalR;
using GameStreamer.Backend.Interfaces;
using GameStreamer.Backend.DTOs;
using GameStreamer.Backend.Storage;
using GameStreamer.Backend.Storage.GameStreamerDbase.Entities;
using Hangfire;
using GameStreamer.Backend.Jobs;
using MassTransit;
using GameStreamer.Backend.DTOs.MessageBus.Consume;

namespace GameStreamer.Backend.Services
{
    public class TestScheduleService : BackgroundService
    {
        
        private readonly IHubContext<GameHub, IGameHub> _gameHub;
        private readonly IHubContext<RoomsHub, IRoomsHub> _roomsHub;
        private readonly Random _random = new Random();
        private readonly IRoomManager _roomsManager;
        private readonly IGameStreamRepository _gameStreamRepository;

        private readonly ICustomJobService _customJobService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;

        private readonly IPublishEndpoint _publishEndpoint;

        public TestScheduleService(
            IHubContext<GameHub,
            IGameHub> gameHub,
            IHubContext<RoomsHub,
            IRoomsHub> roomsHub,
            IRoomManager roomsManager,
            IGameStreamRepository gameStreamRepository,
            
            ICustomJobService customJobService,
            IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager,

            IPublishEndpoint publishEndpoint)
        {
            _gameHub = gameHub;
            _roomsHub = roomsHub;
            _roomsManager = roomsManager;
            _gameStreamRepository = gameStreamRepository;

            _customJobService = customJobService;
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;

            _publishEndpoint = publishEndpoint;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(10));

            _recurringJobManager.AddOrUpdate("clearUnactiveRoomsAndPlayersData", () => _customJobService.ReccuringJob(), Cron.HourInterval(3));

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                Console.WriteLine("TestScheduleService have got next tick! Waiting for 5 sec...");

                await _publishEndpoint.Publish(new TurnDeniedDto {
                    PlayerGuid = Guid.Empty,
                    RoomGuid = Guid.Empty,
                });

                await _publishEndpoint.Publish(new TurnAcceptedDto {
                    PlayerGuid = Guid.Empty,
                    RoomGuid = Guid.Empty,
                });

                var testRoom = new RoomEntity { CreatedAt = DateTime.Now, HubGroupId = "12345", RoomGuid = Guid.NewGuid() };

                var testPlayer1 = new PlayerEntity { Nickname = "Noob1", ChatHubId = "111aaa", GameHubId = "222aaa", RoomHubId = "333aaa", CreatedAt = DateTime.Now, PlayerGuid = Guid.NewGuid() };
                var testPlayer2 = new PlayerEntity { Nickname = "Noob2", ChatHubId = "111bbb", GameHubId = "222bbb", RoomHubId = "333bbb", CreatedAt = DateTime.Now, PlayerGuid = Guid.NewGuid() };

                testRoom.JoinedPlayers.Add(testPlayer1);
                testRoom.JoinedPlayers.Add(testPlayer2);

                _gameStreamRepository.InsertRoom(testRoom);
                _gameStreamRepository.Save();

                await _roomsHub.Clients.All.NewRoomAdded(
                    new GameRoomResponseDTO { 
                        RoomName = $"Room_{_random.Next(8000, 15000)}",
                        PlayersList = new List<PlayerDataResponseDTO> {
                            new PlayerDataResponseDTO { ConnectionId = "123", NickName = "Pidor_666" },
                            new PlayerDataResponseDTO { ConnectionId = "456", NickName = "Pidor_777" },
                        }
                    });

                _backgroundJobClient.Enqueue(() => _customJobService.FireAndForgetJob());

            }
        }
    }
}
