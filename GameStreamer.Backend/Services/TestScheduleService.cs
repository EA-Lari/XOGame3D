using GameStreamer.Backend.Hubs;
using Microsoft.AspNetCore.SignalR;
using GameStreamer.Backend.Interfaces;

namespace GameStreamer.Backend.Services
{
    public class TestScheduleService : BackgroundService
    {
        
        private readonly IHubContext<GameHub, IGameHub> _gameHub;

        public TestScheduleService(IHubContext<GameHub, IGameHub> gameHub)
        {
            _gameHub = gameHub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                Console.WriteLine("TestScheduleService have got next tick! Waiting for 5 sec...");
                
                await _gameHub.Clients.All.TestBroadcastPublish("You are pidrila!");
            }
        }
    }
}
