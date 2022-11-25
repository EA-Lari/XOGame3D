using GameStreamer.Backend.DTOs;
using GameStreamer.Backend.Models;
using System.Collections.Concurrent;

namespace GameStreamer.Backend.Services
{
    public class RoomsManager : IRoomsManager
    {

        private readonly Random _random = new Random();
        private readonly ConcurrentBag<Player> _playersConcurrList = new ConcurrentBag<Player>();

        public bool AddPlayerToServer(string connectionId, string nickName)
        {
            _playersConcurrList.Add(new Player(connectionId, nickName));
            return true;
        }

        public PlayerDataResponseDTO GetPlayerDataBy(string connectionId)
        {
            return GetRandomPlayer();
        }

        public PlayerDataResponseDTO GetRandomPlayer()
        {
            return new PlayerDataResponseDTO { NickName = $"Player_{_random.Next(1, 999)}", ConnectionId = $"id_{_random.Next(3000, 9000)}" };
        }

        public GameRoomResponseDTO GetRandomRoom()
        {
            return new GameRoomResponseDTO
            {
                RoomName = $"TestRoom_{_random.Next(1000, 1999)}",
                PlayersList = new List<PlayerDataResponseDTO> {
                        new PlayerDataResponseDTO { NickName = $"Player_{_random.Next(2000, 2999)}" },
                        new PlayerDataResponseDTO { NickName = $"Player_{_random.Next(3000, 3999)}" },
                    }
            };
        }

        public List<GameRoomResponseDTO> GetAllGameRooms()
        {
            return new List<GameRoomResponseDTO>
            {
                new GameRoomResponseDTO
                { 
                    RoomName = "TestRoom_1",
                    PlayersList = new List<PlayerDataResponseDTO> { 
                        new PlayerDataResponseDTO { NickName = "Player_1" },
                        new PlayerDataResponseDTO { NickName = "Player_2" },
                    }
                },
            };
        }

        public List<PlayerDataResponseDTO> GetAllPlayers()
        {
            return new List<PlayerDataResponseDTO>
            {
                new PlayerDataResponseDTO { NickName = "Player_1" },
                new PlayerDataResponseDTO { NickName = "Player_2" },
            };
        }

        public bool RemovePlayer()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("RoomsManager пока не умеет удалять игроков :(");
            Console.ForegroundColor = ConsoleColor.Gray;
            return false;
        }

        public bool RemoveRoom()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("RoomsManager пока не умеет удалять комнаты :(");
            Console.ForegroundColor = ConsoleColor.Gray;
            return false;
        }
    }
}
