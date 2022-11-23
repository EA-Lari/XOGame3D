using GameStreamer.Backend.DTOs;

namespace GameStreamer.Backend.Services
{
    public class RoomsManager : IRoomsManager
    {

        private readonly Random _random = new Random();

        public PlayerNickNameResponseDTO GetRandomPlayer()
        {
            return new PlayerNickNameResponseDTO { NickName = $"Player_{_random.Next(1, 999)}" };
        }

        public GameRoomResponseDTO GetRandomRoom()
        {
            return new GameRoomResponseDTO
            {
                RoomName = $"TestRoom_{_random.Next(1000, 1999)}",
                PlayersList = new List<PlayerNickNameResponseDTO> {
                        new PlayerNickNameResponseDTO { NickName = $"Player_{_random.Next(2000, 2999)}" },
                        new PlayerNickNameResponseDTO { NickName = $"Player_{_random.Next(3000, 3999)}" },
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
                    PlayersList = new List<PlayerNickNameResponseDTO> { 
                        new PlayerNickNameResponseDTO { NickName = "Player_1" },
                        new PlayerNickNameResponseDTO { NickName = "Player_2" },
                    }
                },
            };
        }

        public List<PlayerNickNameResponseDTO> GetAllPlayers()
        {
            return new List<PlayerNickNameResponseDTO>
            {
                new PlayerNickNameResponseDTO { NickName = "Player_1" },
                new PlayerNickNameResponseDTO { NickName = "Player_2" },
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
