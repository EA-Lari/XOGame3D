using GameStreamer.Backend.DTOs;
using GameStreamer.Backend.Models;
using System.Collections.Concurrent;
using GameStreamer.Backend.Storage;

namespace GameStreamer.Backend.Services
{

    public class PlayerManager : IPlayerManager
    {

        private readonly IGameStreamRepository _gameRepo;
        //private readonly Random _random = new Random();
        //private readonly ConcurrentDictionary<string, PlayerFromRoomHub> _playersConcurrDict = new ConcurrentDictionary<string, PlayerFromRoomHub>();

        public PlayerManager(IGameStreamRepository gameRepo)
        {
            _gameRepo = gameRepo;
        }

        public PlayerDataResponseDTO AddPlayerToServer(string connectionId, string nickName)
        {
            var playerForAdd = new PlayerFromRoomHub(connectionId, nickName);
            var addedPlayerData = _gameRepo.AddPlayer(playerForAdd);

            return addedPlayerData;
        }

        public PlayerDataResponseDTO ChangePlayerNickName(string connectionId, string nickName)
        {

            var existedPlayer = _gameRepo.GetPlayerBy(connectionId);
            existedPlayer.SetNewNickName(nickName);
            _gameRepo.UpdatePlayer(existedPlayer);

            return new PlayerDataResponseDTO { ConnectionId = changedPlayerFromRoomHub.RoomConnectionId, NickName = changedPlayerFromRoomHub.NickName };
        }

        public PlayerDataResponseDTO GetPlayerDataBy(string connectionId)
        {
            return GetRandomPlayer();
        }

        public PlayerDataResponseDTO GetRandomPlayer()
        {
            //return new PlayerDataResponseDTO { NickName = $"Player_{_random.Next(1, 999)}", RoomConnectionId = $"id_{_random.Next(3000, 9000)}" };
        }

        public List<PlayerDataResponseDTO> GetAllPlayersWithoutRoom()
        {
            //return _playersConcurrDict.Select(x => new PlayerDataResponseDTO { RoomConnectionId = x.Key, NickName = x.Value.NickName }).ToList();
        }

        public PlayerDataResponseDTO RemovePlayer(string connectionId)
        {
            PlayerFromRoomHub removedPlayerFromRoomHub;
            //_playersConcurrDict.TryRemove(connectionId, out removedPlayerFromRoomHub);

            return new PlayerDataResponseDTO() { ConnectionId = removedPlayerFromRoomHub.RoomConnectionId, NickName = removedPlayerFromRoomHub.NickName };
        }

        public PlayerDataResponseDTO MakePlayerReadyToGame(string connectionId)
        {
            Console.WriteLine("Метод MakePlayerReadyToGame еще не реализован!");
            return new PlayerDataResponseDTO();
        }

        public PlayerDataResponseDTO SetMatchTypeToPlayer(string connectionId, bool matchType)
        {
            Console.WriteLine("Метод SetMatchTypeToPlayer еще не реализован!");
            return new PlayerDataResponseDTO();
        }

    }
}