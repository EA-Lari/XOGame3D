using GameStreamer.Backend.DTOs;

namespace GameStreamer.Backend.Services
{
    public class RoomManager : IRoomManager
    {
        private readonly Random _random = new();

        public GameRoomResponseDTO GetRandomRoom()
        {
            return new GameRoomResponseDTO
            {
                RoomName = $"",
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

        public GameRoomResponseDTO RemoveRoom(string roomName)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Сорян, RoomsManager пока не умеет удалять комнаты :(");
            Console.ForegroundColor = ConsoleColor.Gray;
            return new GameRoomResponseDTO();
        }

        public GameRoomResponseDTO AddRoom(string roomName)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Сорян, RoomsManager пока не умеет добавлять комнаты :(");
            Console.ForegroundColor = ConsoleColor.Gray;

            throw new NotImplementedException();
        }

    }
}