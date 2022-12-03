using GameStreamer.Backend.DTOs;
using GameStreamer.Backend.Models;
using System.Collections.Concurrent;

namespace GameStreamer.Backend.Services
{
    public class RoomsManager : IRoomsManager
    {

        private readonly Random _random = new Random();
        private readonly ConcurrentDictionary<string,Player> _playersConcurrDict = new ConcurrentDictionary<string, Player>();
        private readonly ConcurrentDictionary<string, Room> _roomsConcurrDict = new ConcurrentDictionary<string, Room>();

        #region Players

        public PlayerDataResponseDTO AddPlayerToServer(string connectionId, string nickName)
        {
            var playerForAdd = new Player(connectionId, nickName);
            _playersConcurrDict.TryAdd(connectionId, playerForAdd);

            return new PlayerDataResponseDTO { ConnectionId = playerForAdd.ConnectionId, NickName = playerForAdd.NickName };
        }

        public PlayerDataResponseDTO GetPlayerDataBy(string connectionId)
        {
            return GetRandomPlayer();
        }

        public PlayerDataResponseDTO GetRandomPlayer()
        {
            return new PlayerDataResponseDTO { NickName = $"Player_{_random.Next(1, 999)}", ConnectionId = $"id_{_random.Next(3000, 9000)}" };
        }

        public List<PlayerDataResponseDTO> GetAllPlayersWithoutRoom()
        {
            return _playersConcurrDict.Select(x => new PlayerDataResponseDTO { ConnectionId = x.Key, NickName = x.Value.NickName }).ToList();
        }

        public PlayerDataResponseDTO RemovePlayer(string connectionId)
        {
            Player removedPlayer;
            _playersConcurrDict.TryRemove(connectionId, out removedPlayer);

            return new PlayerDataResponseDTO() { ConnectionId = removedPlayer.ConnectionId, NickName = removedPlayer.NickName };
        }

        #endregion

        #region Rooms

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

        public GameRoomResponseDTO RemoveRoom(string roomName)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("RoomsManager пока не умеет удалять комнаты :(");
            Console.ForegroundColor = ConsoleColor.Gray;
            return new GameRoomResponseDTO();
        }

        public GameRoomResponseDTO AddRoom(string roomName)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("RoomsManager пока не умеет добавлять комнаты :(");
            Console.ForegroundColor = ConsoleColor.Gray;

            throw new NotImplementedException();
        }

        public PlayerDataResponseDTO ChangePlayerNickName(string connectionId, string nickName)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}